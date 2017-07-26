// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using System.Collections;
using System.Collections.Generic;
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
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            CreateMap<UserTeam, UserTeamDto>();
            CreateMap<UserTeamDto, UserTeam>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<TeamDto, Team>()
                .ForMember(dest => dest.UserTeams, opt => opt.Ignore())
                ;
            CreateMap<Team, TeamDto>()
                .ForMember(dest => dest.Users, opt => opt.Ignore())
                ;

            CreateMap<TeamReadiness, MonthReadinessDto>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.TrackedDate))
                ;

            CreateMap<Player, PlayerReadinessInformationDto>()
                .ForMember(dest => dest.PlayerId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.CurrentReadiness, opt => opt.MapFrom(src => src.Availability))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.PathToPhoto, opt => opt.MapFrom(src => src.User.PathtoPhoto))
                .ForMember(dest => dest.DominantSkill, opt => opt.MapFrom(src => src.DominantSkill.Name))
                ;

            CreateMap<TeamMatch, MatchInfoDto>();


            CreateMap<TeamImagesDto, TeamImages>();
            CreateMap<TeamImages, TeamImagesDto>()
                .ForMember(dest => dest.Images, opt => opt.Ignore())
                ;
            CreateMap<Image, ImageDto>();
            CreateMap<ImageDto, Image>();

        }
    }


    public class TeamService : TPPDbService, ITeamService
    {
        private IDbConnection _db;

        public TeamService(TPPContext context) : base(context)
        {
            _db = context?.TppDbConnection;
            RegisterEntities();
        }

        public TeamService(BaseDbContext context) : base(context)
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


        public async Task<TeamDto> GetAllTeamPlayers()
        {
            var teamDb = await _db.FindAsync<Team>(query => query
                .Include<UserTeam>(join => join.InnerJoin()
                .OrderBy($"{nameof(UserTeam.UserId):C}"))
                .Include<User>(join => join.InnerJoin())
                );

            var enumerable = teamDb as IList<Team> ?? teamDb.ToList();
            var team = enumerable.First();
            if (team == null) return null;

            //Map to the model object
            var theTeam = Mapper.Map<TeamDto>(team);
            theTeam.Users = new List<UserDto>();

            //Add the users to the Team
            foreach (var userTeam in team.UserTeams)
            {
                ((IList)theTeam.Users).Add(Mapper.Map<UserDto>(userTeam.User));
            }

            return theTeam;

        }

        public async Task<TeamDto> GetDomesticTeamPlayers(int id)
        {
            var teamDb = await _db.FindAsync<Team>(query => query
                .Where($"{nameof(Team.Id):C} = @TeamId")
                .Include<UserTeam>(join => join.InnerJoin()
                .Where($"{nameof(UserTeam.TeamId):C} = @TeamId")
                .OrderBy($"{nameof(UserTeam.UserId):C}"))
                .Include<User>(join => join.InnerJoin())
                .WithParameters(new
                {
                    TeamId = id
                }));

            var enumerable = teamDb as IList<Team> ?? teamDb.ToList();
            var team = enumerable.First();
            if (team == null) return null;

            //Map to the model object
            var theTeam = Mapper.Map<TeamDto>(team);
            theTeam.Users = new List<UserDto>();

            //Add the users to the Team
            foreach (var userTeam in team.UserTeams)
            {
                ((IList)theTeam.Users).Add(Mapper.Map<UserDto>(userTeam.User));
            }

            return theTeam;

        }

        public async Task<IEnumerable<MonthReadinessDto>> GetTeamReadiness(int id)
        {
            var teamDb = await _db.FindAsync<TeamReadiness>(query => query
                .Where($"{nameof(TeamReadiness.TeamId):C} = @teamId")
                .WithParameters(new
                {
                    teamId = id
                }));

            return teamDb.Select(rec =>
                Mapper.Map<MonthReadinessDto>(rec)).ToList();
        }

        public async Task<IEnumerable<PlayerReadinessInformationDto>> GetPlayersReadiness(int id)
        {
            var recDb = await _db.FindAsync<Player>(query => query
                .Where($"{nameof(Player.Id):C} = @teamId")
                .WithParameters(new
                {
                    teamId = id
                }));

            return recDb.Select(rec =>
                Mapper.Map<PlayerReadinessInformationDto>(rec)).ToList();

            ////get the player's user and skills info
            //foreach (var playerInfo in playersInfo)
            //{
            //    var player = await _db.FindAsync<User>(query => query
            //        .Include<Player>(join => join.InnerJoin()
            //        .Where($"{nameof(Player.UserId):C} = @userId"))
            //        .WithParameters(new
            //        {
            //            userId = playerInfo.PlayerId
            //        }));
            //    playerInfo.FirstName = player.SingleOrDefault().FirstName;
            //    playerInfo.LastName = player.SingleOrDefault().LastName;
            //    playerInfo.PathToPhoto = player.SingleOrDefault().PathtoPhoto;

            //    //Get the dominant skill
            //    var skill = await _db.GetAsync(new Skill() {Id = Int32.Parse(playerInfo.DominantSkill)});
            //    playerInfo.DominantSkill = skill.Name;
            //}

            //return playersInfo;
        }

        public async Task<MatchInfoDto> GetNextMatchInfo(int teamId, DateTime currentDate)
        {
            var recDb = await _db.FindAsync<TeamMatch>(query => query
                .Include<Team>(join => join.InnerJoin())
                .Include<Location>(join => join.InnerJoin())
                .Where($"{nameof(TeamMatch.FirstTeamId):C} = @teamId and {nameof(TeamMatch.DateTime):C} > @currentDate")
                .WithParameters(new
                {
                    teamId,
                    currentDate
                })
                .OrderBy($"{nameof(TeamMatch.DateTime):C}"));

            var matchInfo = recDb.SingleOrDefault();

            return Mapper.Map<MatchInfoDto>(matchInfo);
        }



        public async Task<IEnumerable<MatchInfoDto>> GetMatchHistoryInfo(int teamId)
        {
            var recDb = await _db.FindAsync<TeamMatch>(query => query
                .Include<Team>(join => join.InnerJoin())
                .Include<Location>(join => join.InnerJoin())
                .Where($"{nameof(TeamMatch.FirstTeamId):C} = @teamId")
                .WithParameters(new
                {
                    teamId
                })
                .OrderBy($"{nameof(TeamMatch.DateTime):C}"));


            return recDb.Select(rec =>
                Mapper.Map<MatchInfoDto>(rec)).ToList();
        }


        public async Task<int> CreateTeam(TeamDto teamDto)
        {
            //Map to the Data Entity object
            var recDb = Mapper.Map<Team>(teamDto);

            //Insert into Message table
            await _db.InsertAsync(recDb);

            return recDb.Id;
        }

        public async Task<bool> UpdateTeam(TeamDto teamDto)
        {
            //Map to the DB object
            var newRec = Mapper.Map<Team>(teamDto);

            //Update the database
            return await _db.UpdateAsync(newRec);
        }

        public async Task<bool> DeleteTeam(int teamId)
        {
            //Get the questionnaire by its Id
            var dbRec = await _db.GetAsync(new Team() { Id = teamId });
            //Delete the the record from the DB
            return await _db.DeleteAsync(dbRec);
        }

        public async Task<bool> AddTeamImages(TeamImagesDto teamImages)
        {
            var result = true;

            //Map to the Data Entity object
            var recDb = Mapper.Map<TeamImages>(teamImages);

            foreach (var image in teamImages.Images)
            {
                //Update a Image DB Record by creating a new one and updating the TeamImage ID, but only if the URL has changed
                var imageRec = await _db.FindAsync<Image>(query => query
                    .Where($"{nameof(Image.Url):C} = @imageUrl")
                    .WithParameters(new { imageUrl = image.Url }));
                var imageId = (imageRec != null && imageRec.Any()) ? imageRec.First().Id : 0;
                if (imageId == 0)
                {
                    var imgDb = new Image() { Url = image.Url };
                    await _db.InsertAsync(imgDb);
                    result &= imgDb.Id > 0;
                    imageId = imgDb.Id;
                }

                //Update the new record
                recDb.ImageId = imageId;

                //Insert the new record into the TeamImages table
                await _db.InsertAsync(recDb);
                result &= recDb.Id > 0;
            }

            return result;
        }

        public async Task<TeamImagesDto> GetTeamImages(int teamId)
        {
            var recDb = await _db.FindAsync<TeamImages>(query => query
                .Include<Image>(join => join.InnerJoin())
                .Where($"{nameof(TeamImages.TeamId):C} = @teamId And {nameof(TeamImages.IsActive):C} = @isActive")
                .WithParameters(new
                {
                    teamId,
                    isActive = true
                })
                .OrderBy($"{nameof(TeamImages.DateCreated):C} DESC"));

            if (!recDb.Any()) return null;

            var teamImages = Mapper.Map<TeamImagesDto>(recDb.First());
            teamImages.Images = new List<ImageDto>();

            //Map to the DTO object
            foreach (var teamImageDb in recDb)
            {
                ((List<ImageDto>) teamImages.Images).Add(Mapper.Map<ImageDto>(teamImageDb.Image));
            }

            return teamImages;
        }


        public async Task<IList<TeamDto>> GetTeams()
        {
            var recDb = await _db.FindAsync<Team>();

            return recDb.Select(rec =>
                Mapper.Map<TeamDto>(rec)).ToList();
        }



        public async Task<TeamDto> GetTeam(int teamId)
        {
            var recDb = await _db.GetAsync(new Team() {Id = teamId});

            return Mapper.Map<TeamDto>(recDb);
        }



        public async Task<TeamDto> GetTeamByName(string teamName)
        {
            var recDb = await _db.FindAsync<Team>(query => query
                .Where($"{nameof(Team.Name):C} = @teamName")
                .WithParameters(new {teamName}));

            if (recDb == null || !recDb.Any()) return null;

            return Mapper.Map<TeamDto>(recDb.First());
        }
    }
}
