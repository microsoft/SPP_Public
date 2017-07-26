// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dapper.FastCrud;
using TPP.Core.Data;
using TPP.Core.Data.Entities;
using TPP.Core.Services.Contracts;
using TPP.Core.Services.Models;

namespace TPP.Core.Services.Impl
{
    public class CognitiveProfile : Profile
    {
        public CognitiveProfile()
        {
            CreateMap<CognitiveServiceKeysDto, Data.Entities.CognitiveService>();
            CreateMap<Data.Entities.CognitiveService, CognitiveServiceKeysDto>();
            CreateMap<EmotionsDto, UserEmotion>()
                .ForMember(dest => dest.TakenOn, opt => opt.MapFrom(src => src.CapturedOn));
            CreateMap<UserEmotion, EmotionsDto>()
                .ForMember(dest => dest.CapturedOn, opt => opt.MapFrom(src => src.TakenOn));
        }
    }


    public class CognitiveService : TPPDbService, ICognitiveService
    {
        private IDbConnection _db;

        public CognitiveService(TPPContext context) : base(context)
        {
            _db = context?.TppDbConnection;
            RegisterEntities();
        }

        public CognitiveService(BaseDbContext context) : base(context)
        {
            _db = context.DbConnection;
            RegisterEntities();
        }

        public void RegisterEntities()
        {
            OrmConfiguration.DefaultDialect = SqlDialect.MsSql;

            //Initialize static mapping
            AutoMapperConfiguration.Configure();
        }


        #region CREATE


        /// <summary>
        /// 
        /// </summary>
        /// <param name="newKeysDto"></param>
        /// <returns></returns>
        public async Task<bool> CreateCognitiveServiceKeys(CognitiveServiceKeysDto newKeysDto)
        {
            //Map the DTO object into the data entity
            var cogSvcDb = Mapper.Map<Data.Entities.CognitiveService>(newKeysDto);

            //First, insert into child table
            await _db.InsertAsync(cogSvcDb);


            //Now, insert into the TeamCognitiveServices Table
            var teamCgnSvcDb = new TeamCognitiveService()
            {
                TeamId = newKeysDto.TeamId,
                CognitiveServiceId = cogSvcDb.Id
            };
            await _db.InsertAsync(teamCgnSvcDb);

            return cogSvcDb.Id > 0;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="emotionsDto"></param>
        /// <returns></returns>
        public async Task<bool> AddUserEmotions(EmotionsDto emotionsDto)
        {
            //Map the DTO object into Data Entity
            var userEmotion = Mapper.Map<UserEmotion>(emotionsDto);
            userEmotion.TakenOn = DateTime.UtcNow;

            //Insert into DB
            await _db.InsertAsync(userEmotion);

            return true;

        }

        #endregion


        #region RETRIEVE

        /// <summary>
        /// 
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        public async Task<CognitiveServiceKeysDto> GetCognitiveServiceKeys(int teamId)
        {
            //Get Cognitive Services
            var keysDb = await _db.FindAsync<Data.Entities.CognitiveService>(query => query
                .Include<TeamCognitiveService>(join => join.InnerJoin()
                .Where($"{nameof(TeamCognitiveService.TeamId):C} = @TeamId"))
                .OrderBy($"{nameof(Data.Entities.CognitiveService.Id):C} DESC")
                .WithParameters(new
                {
                    TeamId = teamId
                }));

            var enumerable = keysDb as IList<Data.Entities.CognitiveService> ?? keysDb.ToList();
            var keys = (enumerable.Any()) ? enumerable.First() : null;
            if (keys == null) return null;

            //Map to the DTO object
            var keysDto = Mapper.Map<CognitiveServiceKeysDto>(keys);
            keysDto.TeamId = teamId;
            return keysDto;
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="userIdentities"></param>
        /// <returns></returns>
        public async Task<int> AuthenticateUserByFace(IList<UserIdentityDto> userIdentities)
        {
            foreach (var identity in userIdentities.OrderByDescending(x => x.Confidence))
            {
                //Get the Team
                var userTeams = await _db.FindAsync<UserTeam>(query => query
                    .Where($"{nameof(UserTeam.DateStart):C} is Null Or {nameof(UserTeam.DateStart):C} <= @UtcNow And {nameof(UserTeam.DateEnd):C} is Null Or {nameof(UserTeam.DateEnd):C} > @UtcNow")
                    .Include<User>(join => join.InnerJoin()
                    .Where($"{nameof(User.FullName):C} = @FullName"))
                    .Include<Team>(join => join.InnerJoin())
                    .WithParameters(new
                    {
                        UtcNow = DateTime.UtcNow,
                        FullName = identity.FullName
                    }));


                var enumerable = userTeams as IList<UserTeam> ?? userTeams.ToList();
                var userTeam = (enumerable.Any()) ? enumerable.First() : null;

                if (userTeam != null && identity.Emotions != null && userTeam.TeamId > 0)
                {
                    var emotions = identity.Emotions;
                    emotions.UserId = userTeam.UserId;
                    emotions.TeamId = userTeam.TeamId;
                    await AddUserEmotions(emotions);
                }
                return userTeam?.TeamId ?? 0;
            }

            return 0;
        }



        #endregion

        #region UPDATE


        /// <summary>
        /// 
        /// </summary>
        /// <param name="newKeysDto"></param>
        /// <returns></returns>
        public async Task<bool> UpdateCognitiveServiceKeys(CognitiveServiceKeysDto newKeysDto)
        {
            //Map the DTO object into the data entity
            var cogSvcDb = Mapper.Map<Data.Entities.CognitiveService>(newKeysDto);
            return await _db.UpdateAsync(cogSvcDb);

        }


        #endregion


        #region DELETE
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteCognitiveServiceKeys(int keyId)
        {
            //Find the cognitive services key record to be deleted
            var cogSvcDb = new Data.Entities.CognitiveService() { Id = keyId };
            var cogSvcs = await _db.GetAsync(cogSvcDb);

            //Delete from TeamCognitiveServices first
            var teamCs = await _db.FindAsync<TeamCognitiveService>(query => query
                .Where($"{nameof(TeamCognitiveService.CognitiveServiceId):C} = @Id")
                .WithParameters(new {Id = cogSvcs.Id}));
            foreach (var teamCognitiveService in teamCs)
            {
                await _db.DeleteAsync(teamCognitiveService);
            }

            //Delete the Cognitive Services record from the DB
            return await _db.DeleteAsync(cogSvcs);

        }


        #endregion



    }

}