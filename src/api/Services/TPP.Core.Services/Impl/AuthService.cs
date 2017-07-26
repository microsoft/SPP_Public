// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using Dapper.FastCrud;
using Microsoft.Extensions.Logging;
using TPP.Core.Data;
using TPP.Core.Data.Entities;
using TPP.Core.Services.Contracts;
using TPP.Core.Services.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TPP.Core.Services.Impl.B2C;


namespace TPP.Core.Services.Impl
{
    public enum UserRoles
    {
        Coach = 1,
        Player = 2,
        Admin = 3
    }

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.PlayerInfo, opt => opt.Ignore());
            CreateMap<PlayerDto, Player>()
                .ForMember(dest => dest.BowlingGroup,
                    opt => opt.MapFrom(src => (src.Cricket != null) ? src.Cricket.BowlingGroup : String.Empty))
                .ForMember(dest => dest.BowlingHand,
                    opt => opt.MapFrom(src => (src.Cricket != null) ? src.Cricket.BowlingHand : String.Empty))
                .ForMember(dest => dest.BattingGroup,
                    opt => opt.MapFrom(src => (src.Cricket != null) ? src.Cricket.BattingGroup : String.Empty))
                .ForMember(dest => dest.BattingHand,
                    opt => opt.MapFrom(src => (src.Cricket != null) ? src.Cricket.BattingHand : String.Empty));

        }
    }


    public class AuthService : TPPDbService, IAuthService
    {
        private IDbConnection _db;

        public AuthService(TPPContext context) : base(context)
        {
            _db = context?.TppDbConnection;
            RegisterEntities();
        }

        public AuthService(BaseDbContext context) : base(context)
        {
            _db = context.DbConnection;
            RegisterEntities();
        }

        public void RegisterEntities()
        {
            OrmConfiguration.DefaultDialect = SqlDialect.MsSql;

            //Register DB Entities
            //OrmConfiguration.GetDefaultEntityMapping<User>()
            //    .SetTableName("User")
            //    .SetProperty(user => user.Id,
            //        prop => prop.SetPrimaryKey().SetDatabaseGenerated(DatabaseGeneratedOption.Identity));

            //OrmConfiguration.GetDefaultEntityMapping<UserTeam>()
            //    .SetTableName("UserTeam")
            //    .SetProperty(user => user.Id,
            //        prop => prop.SetPrimaryKey().SetDatabaseGenerated(DatabaseGeneratedOption.Identity));


            //Initialize static mapping
            AutoMapperConfiguration.Configure();
        }

        #region CRUD

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newUserDto"></param>
        /// <returns></returns>
        public async Task<int> CreateUser(UserDto newUserDto)
        {
            //Map to the Data Entity object
            User user = Mapper.Map<User>(newUserDto);


            //Add a new user into Azure AD
            var userAD = await CreateUserAD(newUserDto);

            //Update the AD Id property
            user.AADId = userAD.objectId;

            //Insert into User table
            await _db.InsertAsync(user);

            //Now, insert into the UserTeam Table
            var userTeam = new UserTeam()
            {
                UserId = user.Id,
                TeamId = newUserDto.TeamId,
                DateStart = user.StartDate,
                DateEnd = user.EndDate
            };

            await _db.InsertAsync(userTeam);


            //Insert Player's related fields into the Player table
            if (newUserDto.RoleId == (int) UserRoles.Player && newUserDto.PlayerInfo != null)
            {
                var player = Mapper.Map<Player>(newUserDto.PlayerInfo);
                player.UserId = user.Id;
                player.User = user;

                await _db.InsertAsync(player);
            }

            return user.Id;
        }


 

 
        /// <summary>
        /// Looks for the userDto matching the First+Last Name concat, and returns the TeamDto Id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<int> AuthenticateUser(string userId)
        {
            try
            {
                //Get the Team
                var team = await _db.FindAsync<Team>(query => query
                    .Include<UserTeam>(join => join.InnerJoin()
                    .Where($"{nameof(UserTeam.DateStart):C} is Null Or {nameof(UserTeam.DateStart):C} <= @UtcNow And {nameof(UserTeam.DateEnd):C} is Null Or {nameof(UserTeam.DateEnd):C} > @UtcNow"))
                    .Include<User>(join => join.InnerJoin()
                    .Where($"{nameof(User.AADId):C} = @AADId"))
                    .WithParameters(new
                    {
                        UtcNow = DateTime.UtcNow,
                        AADId = userId
                    }));


                var enumerable = team as IList<Team> ?? team.ToList();
                return (enumerable.Any()) ? enumerable.First().Id : 0;

            }
            catch (Exception ex)
            {
                throw new TPPApiException(ex);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUser(UserDto userDto)
        {
            //Map to the Data Entity object
            User updUser = Mapper.Map<User>(userDto);

            //Update the UserDto Database's table
            if (await _db.UpdateAsync(updUser))
            {
                //Get the UserTeamDto record
                var userTeams = await _db.FindAsync<UserTeam>(query => query
                    .Where($"{nameof(UserTeam.UserId):C} = @UserId")
                    .WithParameters(new
                    {
                        UserId = updUser.Id
                    }));
                
                //Update the team Id
                foreach (var team in userTeams)
                {
                    team.TeamId = userDto.TeamId;
                    await _db.UpdateAsync(team);
                }

                //Update Player's info
                if (userDto.RoleId == (int)UserRoles.Player && userDto.PlayerInfo != null)
                {
                    var player = Mapper.Map<Player>(userDto.PlayerInfo);
                    player.UserId = updUser.Id;
                    player.User = updUser;

                    await _db.UpdateAsync(player);
                }

                return true;
            }
            return false;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<int> DeleteUser(int userId)
        {
            //Find the user to be deleted
            var userDb = await _db.FindAsync<User>(query => query
                .Where($"{nameof(User.Id):C} = @userId")
                .WithParameters(new { userId }));

            if (userDb.Any())
            {
                var user = userDb.First();

                //Delete Player's info
                if (user.RoleId == (int)UserRoles.Player)
                {
                    var playerDb = await _db.FindAsync<Player>(query => query
                        .Where($"{nameof(Player.UserId):C} = @userId")
                        .WithParameters(new { userId }));

                    if(playerDb.Any())
                        await _db.DeleteAsync(playerDb.First());
                }


                //Delete the user's record from the DB
                await _db.DeleteAsync(user);

                //Now, delete from UserTeam table
                var userTeams = await _db.FindAsync<UserTeam>(query => query
                    .Where($"{nameof(UserTeam.UserId):C} = @UserId")
                    .WithParameters(new
                    {
                        UserId = user.Id
                    }));

                //Delete all found users in the UserTeam
                foreach (var team in userTeams)
                {
                    await _db.DeleteAsync(team);
                }

                //Delete user from AD
                await DeleteUserAD(user.AADId);

                return user.Id;
            }

            return 0;
        }

        public async Task<Models.UserDto> GetUser(int userId)
        {
            //Get the user from the DB
            var userDb = new User() { Id = userId };
            userDb = await _db.GetAsync(userDb);

            //Map to the model object
            var user = Mapper.Map<UserDto>(userDb);
            return user;
        }



        public async Task<AppCredentialsDTO> GetToken(AppCredentialsDTO appCredentials)
        {
            if (appCredentials == null) return null;

            var authority = string.Format(CultureInfo.InvariantCulture, appCredentials.AADInstance,
                appCredentials.Tenant);
            var authContext = new AuthenticationContext(authority, TokenCache.DefaultShared);
            var credentials = new ClientCredential(appCredentials.ClientId, appCredentials.ClientKey);
            var token = await authContext.AcquireTokenAsync(appCredentials.Audience, credentials);
            appCredentials.Token = token?.AccessToken;
            return appCredentials;
        }

        #endregion



        private async Task<ADUser> CreateUserAD(UserDto user)
        {
            //Obtain B2C Settings
            var b2cDB = new ADB2CSettings() { Id = 1 };
            var b2cSettings = await _db.GetAsync(b2cDB);

            //Create a new user object
            var userObject = new JObject
            {
                {"accountEnabled", true},
                {"creationType", "LocalAccount"},
                {"displayName", user.FullName},
                {"passwordProfile", new JObject
                    {
                        {"password", "WSXzaq!23"},
                        {"forceChangePasswordNextLogin", true}
                    }
                },
                {"signInNames", new JArray
                    {
                        new JObject
                        {
                            {"type", "emailAddress"},
                            {"value", user.Email.Trim()}
                        }
                    }
                }
            };

            //Use Microsoft Graph to perform action on Azure AD B2C
            var client = new B2CGraphClient(
                                b2cSettings.AadClientId, 
                                b2cSettings.AadClientSecret, 
                                b2cSettings.AadTenant,
                                b2cSettings.AadGraphResourceId,
                                b2cSettings.AadGraphEndpoint,
                                b2cSettings.AadGraphVersion);
            var response = await client.CreateUser(userObject.ToString());
            var newUser = JsonConvert.DeserializeObject<ADUser>(response);

            return newUser;
        }


        private async Task DeleteUserAD(string objectId)
        {
            try
            {
                //Obtain B2C Settings
                var b2cDB = new ADB2CSettings() { Id = 1 };
                var b2cSettings = await _db.GetAsync(b2cDB);

                //Use Microsoft Graph to perform action on Azure AD B2C
                var client = new B2CGraphClient(
                                    b2cSettings.AadClientId,
                                    b2cSettings.AadClientSecret,
                                    b2cSettings.AadTenant,
                                    b2cSettings.AadGraphResourceId,
                                    b2cSettings.AadGraphEndpoint,
                                    b2cSettings.AadGraphVersion);
                await client.DeleteUser(objectId);
            }
            catch (Exception)
            {
                //Ignore if no AAD user is found
            }
        }

    }
}