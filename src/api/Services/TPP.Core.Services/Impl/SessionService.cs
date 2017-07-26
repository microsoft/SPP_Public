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

    public class SessionProfile : Profile
    {
        public SessionProfile()
        {
            CreateMap<SessionUser, SessionUserDto>();
            CreateMap<SessionUserDto, SessionUser>();

            CreateMap<LocationDto, Location>();
            CreateMap<Location, LocationDto>();

            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();

            CreateMap<SessionDto, Session>()
                .ForMember(dest => dest.LocationId, opt => opt.Ignore())
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.SessionType))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.Scheduled))
                ;

            CreateMap<Session, SessionDto>()
                .ForMember(dest => dest.Users, opt => opt.Ignore())
                .ForMember(dest => dest.SessionType, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Scheduled, opt => opt.MapFrom(src => src.StartTime))
                ;


        }
    }

    public class SessionService : TPPDbService, ISessionService
    {
        private IDbConnection _db;

        public SessionService(TPPContext context) : base(context)
        {
            _db = context?.TppDbConnection;
            RegisterEntities();
        }

        public SessionService(BaseDbContext context) : base(context)
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionDate"></param>
        /// <returns></returns>
        public async Task<List<SessionDto>> GetSessionsByDate(DateTime sessionDate)
        {
            var sessionsList = new List<SessionDto>();

            //Get the player's questions
            var sessions = await _db.FindAsync<SessionUser>(query => query
                .Include<Session>(join => join.InnerJoin()
                .Where($"{nameof(Session.StartTime):C} = @sessionDate"))
                .Include<Location>(join => join.InnerJoin())
                .Include<User>(join => join.InnerJoin())
                .WithParameters(new { sessionDate }));

            //Find the navigation properties (Users, Location) for each session in the returning list
            foreach (var dbSessions in sessions.GroupBy(x => x.SessionId))
            {
                var session = Mapper.Map<SessionDto>(dbSessions.First().Session);
                session.Users = new List<UserDto>();

                foreach (var sessionDb in dbSessions)
                {
                    if ((bool)sessionDb.User?.isActive && (bool)sessionDb.User?.isEnabled)
                        ((IList)session.Users).Add(Mapper.Map<UserDto>(sessionDb.User));
                }

                sessionsList.Add(session);
            }

            return sessionsList;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public async Task<List<SessionDto>> GetSessionsRange(DateTime fromDate, DateTime toDate)
        {
            var sessionsList = new List<SessionDto>();

            //Get the player's questions
            var sessions = await _db.FindAsync<SessionUser>(query => query
                .Include<Session>(join => join.InnerJoin()
                .Where($"{nameof(Session.StartTime):C} >= @fromDate and {nameof(Session.StartTime):C} <= @toDate"))
                .Include<Location>(join => join.InnerJoin())
                .Include<User>(join => join.InnerJoin())
                .WithParameters(new { fromDate, toDate }));

            //Find the navigation properties (Users, Location) for each session in the returning list
            foreach (var dbSessions in sessions.GroupBy(x => x.SessionId))
            {
                var session = Mapper.Map<SessionDto>(dbSessions.First().Session);
                session.Users = new List<UserDto>();

                foreach (var sessionDb in dbSessions)
                {
                    if((bool)sessionDb.User?.isActive && (bool)sessionDb.User?.isEnabled)
                        ((IList)session.Users).Add(Mapper.Map<UserDto>(sessionDb.User));
                }



                sessionsList.Add(session);
            }

            return sessionsList;

        }


        public async Task<bool> CreateSessionUser(SessionUserDto sessionUserDto)
        {
            //Map to the Data Entity object
            var recDb = Mapper.Map<SessionUser>(sessionUserDto);

            //Insert into Message table
            await _db.InsertAsync(recDb);

            return recDb.Id > 0;
        }

        public async Task<int> CreateSession(SessionDto sessionDto)
        {
            //Get the corresponding location Id
            var dbLocation = await _db.GetAsync(new Location() { Id = (int)sessionDto.Location?.Id });

            //Map to the Data Entity object
            var recDb = Mapper.Map<Session>(sessionDto);
            recDb.LocationId = dbLocation?.Id;

            //Insert into Message table
            await _db.InsertAsync(recDb);

            return recDb.Id;
        }

        public async Task<bool> UpdateSession(SessionDto sessionDto)
        {
            //Map to the DB object
            var newRec = Mapper.Map<Session>(sessionDto);

            //Update the database
            return await _db.UpdateAsync(newRec);
        }

        public async Task<bool> DeleteSession(int sessionId)
        {
            //Get the questionnaire by its Id
            var dbRec = await _db.GetAsync(new Session() { Id = sessionId });
            //Delete the the record from the DB
            return await _db.DeleteAsync(dbRec);
        }

        public async Task<SessionDto> GetSession(int sessionId)
        {
            //Get the questionnaire by its Id
            var dbRec = await _db.GetAsync(new Session() { Id = sessionId });

            return Mapper.Map<SessionDto>(dbRec);
        }

        public async Task AddUserToSession(int sessionId, int userId)
        {
            var sessionUser = new SessionUser()
            {
                SessionId = sessionId,
                UserId = userId
            };

            await _db.InsertAsync(sessionUser);
        }

        public async Task RemoveUserFromSession(int sessionId, int userId)
        {
            //Get the questionnaire by its Id
            var dbRec = await _db.GetAsync(new SessionUser()
            {
                SessionId = sessionId,
                UserId = userId
            });

            if(dbRec != null) await _db.DeleteAsync(dbRec);
        }
    }
}
