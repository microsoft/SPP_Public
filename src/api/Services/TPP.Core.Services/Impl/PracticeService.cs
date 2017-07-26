// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

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

    public class PracticeProfile : Profile
    {
        public PracticeProfile()
        {
            CreateMap<NoteDto, Note>();
            CreateMap<Note, NoteDto>();
            CreateMap<Session, SessionDto>();
            CreateMap<User, UserDto>();
            CreateMap<Drill, DrillDto>();
            CreateMap<DrillDto, Drill>();
            CreateMap<Image, ImageDto>();
            CreateMap<PracticeDrill, PracticeDrillDto>();

            CreateMap<PracticeDrillDto, PracticeDrill>();
            CreateMap<PracticeDto, Practice>()
                .ForMember(dest => dest.Modified, opt => opt.MapFrom(src => src.IsModified))
                ;
            CreateMap<Practice, PracticeDto>()
                .ForMember(dest => dest.IsModified, opt => opt.MapFrom(src => src.Modified))
                ;
        }
    }

    public class PracticeService : TPPDbService, IPracticeService
    {
        private IDbConnection _db;

        public PracticeService(TPPContext context) : base(context)
        {
            _db = context?.TppDbConnection;
            RegisterEntities();
        }

        public PracticeService(BaseDbContext context) : base(context)
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
        /// <param name="practiceDto"></param>
        /// <param name="noteDto"></param>
        /// <returns></returns>
        public async Task<int> CreatePractice(PracticeDto practiceDto)
        {
            //Map to the DB entity
            Practice practiceDb = Mapper.Map<Practice>(practiceDto);

            //Obtain the team name
            var teamDb = await _db.GetAsync<Team>(new Team() {Id = practiceDto.TeamId});
            practiceDb.Side = teamDb?.Name;

            //1. Insert into the Practice table
            await _db.InsertAsync(practiceDb);

            if (practiceDb.Id == 0) return 0;

            //2. Insert into PracticeDrill table
            foreach (var drill in practiceDto.PracticeDrills)
            {
                var drillDb = Mapper.Map<PracticeDrill>(drill);
                drillDb.PracticeId = practiceDb.Id;
                await _db.InsertAsync(drillDb);
            }

            return practiceDb.Id;

        }

        #endregion


        #region RETRIEVE


        /// <summary>
        /// 
        /// </summary>
        /// <param name="practiceId"></param>
        /// <returns></returns>
        public async Task<PracticeDto> GetPractice(int practiceId)
        {
            //Get the Practice and all its associated records
            var practiceDb = await _db.FindAsync<PracticeDrill>(query => query
                .Include<Practice>(join => join.InnerJoin()
                .Where($"{nameof(Practice.Id):C} = @practiceId"))
                .Include<Drill>(join => join.InnerJoin())
                .Include<Image>(join => join.LeftOuterJoin())
                .WithParameters(new { practiceId }));

            if (practiceDb == null && !practiceDb.Any()) return null;

            //Map the parent object
            var practice = Mapper.Map<PracticeDto>(practiceDb.First().Practice);

            //Get practice note
            var noteDb = (practice.NoteId != null) ? _db.Get(new Note() { Id = (int)practice.NoteId }) : null;
            practice.Note = (noteDb != null) ? Mapper.Map<NoteDto>(noteDb) : null;
            
            //Initialize the drills collection
            practice.PracticeDrills = new List<PracticeDrillDto>();

            //Map all children objects
            foreach (var practiceDrill in practiceDb)
            {
                var practiceDrillDto = Mapper.Map<PracticeDrillDto>(practiceDrill);
                //Get drill note
                var dnoteDb = (practiceDrill.NoteId != null) ? _db.Get(new Note() { Id = (int)practiceDrill.NoteId }) : null;
                practiceDrillDto.Note = (dnoteDb != null) ? Mapper.Map<NoteDto>(dnoteDb) : null;

                //Add practice drill to the practice drills parent collection
                practice.PracticeDrills.Add(practiceDrillDto);
            }


            return practice;
        }




        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IList<PracticeDto>> GetAllPractices()
        {
            var practicesDb = await _db.FindAsync<Practice>();

            return practicesDb.Select(rec =>
                Mapper.Map<PracticeDto>(rec)).ToList();
        }





        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IList<PracticeDto>> GetAllPracticeDetails()
        {
            var athletePractices = new List<PracticeDto>();

            //Get All Practices
            //Find all records belonging to the sessionId
            var practicesDb = await _db.FindAsync<PracticeDrill>(query => query
                .Include<Practice>(join => join.InnerJoin())
                .Include<Drill>(join => join.InnerJoin())
                .Include<Image>(join => join.InnerJoin())
                );

            foreach (var practiceDb in practicesDb.GroupBy(x => x.PracticeId))
            {
                //Map the parent object
                var practice = Mapper.Map<PracticeDto>(practiceDb.First().Practice);
                //Get practice note
                var noteDb = (practice.NoteId != null) ? _db.Get(new Note() { Id = (int)practice.NoteId }) : null;
                practice.Note = (noteDb != null) ? Mapper.Map<NoteDto>(noteDb) : null;
                //Initialize the drills collection
                practice.PracticeDrills = new List<PracticeDrillDto>();

                //Map all children objects
                foreach (var practiceDrill in practiceDb)
                {
                    var practiceDrillDto = Mapper.Map<PracticeDrillDto>(practiceDrill);
                    //Get drill note
                    var dnoteDb = (practiceDrill.NoteId != null) ? _db.Get(new Note() { Id = (int)practiceDrill.NoteId }) : null;
                    practiceDrillDto.Note = (dnoteDb != null) ? Mapper.Map<NoteDto>(dnoteDb) : null;

                    //Add practice drill to the practice drills parent collection
                    practice.PracticeDrills.Add(practiceDrillDto);
                }

                //Add practices to the practice drill collection
                athletePractices.Add(practice);

            }

            return athletePractices;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IList<PracticeDto>> GetSessionPractices(int sessionId)
        {
            var athletePractices = new List<PracticeDto>();

            //Get All Practices
            //Find all records belonging to the sessionId
            var practicesDb = await _db.FindAsync<PracticeDrill>(query => query
                .Include<Practice>(join => join.InnerJoin()
                .Where($"{nameof(Practice.SessionId):C} = @sessionId"))
                .Include<Drill>(join => join.InnerJoin())
                .Include<Image>(join => join.InnerJoin())
                .WithParameters(new { sessionId }));

            foreach (var practiceDb in practicesDb.GroupBy(x => x.PracticeId))
            {
                //Map the parent object
                var practice = Mapper.Map<PracticeDto>(practiceDb.First().Practice);
                //Get practice note
                var noteDb = (practice.NoteId != null) ? _db.Get(new Note() { Id = (int)practice.NoteId }) : null;
                practice.Note = (noteDb != null) ? Mapper.Map<NoteDto>(noteDb) : null;
                //Initialize the drills collection
                practice.PracticeDrills = new List<PracticeDrillDto>();

                //Map all children objects
                foreach (var practiceDrill in practiceDb)
                {
                    var practiceDrillDto = Mapper.Map<PracticeDrillDto>(practiceDrill);
                    //Get drill note
                    var dnoteDb = (practiceDrill.NoteId != null) ? _db.Get(new Note() { Id = (int)practiceDrill.NoteId }) : null;
                    practiceDrillDto.Note = (dnoteDb != null) ? Mapper.Map<NoteDto>(dnoteDb) : null;

                    //Add practice drill to the practice drills parent collection
                    practice.PracticeDrills.Add(practiceDrillDto);
                }

                //Add practices to the practice drill collection
                athletePractices.Add(practice);

            }

            return athletePractices;

        }





        /// <summary>
        /// 
        /// </summary>
        /// <param name="practiceId"></param>
        /// <returns></returns>
        public async Task<IList<PracticeDrillDto>> GetPracticeDrills(int practiceId)
        {
            //Get All Practices
            //Find all records belonging to the sessionId
            var practicesDb = await _db.FindAsync<PracticeDrill>(query => query
                .Where($"{nameof(PracticeDrill.PracticeId):C} = @practiceId")
                .Include<Drill>(join => join.InnerJoin())
                .Include<Image>(join => join.InnerJoin())
                .Include<Note>(join => join.LeftOuterJoin())
                .WithParameters(new { practiceId }));

            return practicesDb.Select(rec =>
                Mapper.Map<PracticeDrillDto>(rec)).ToList();
        }





        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IList<PracticeDrillDto>> GetPracticeDrills()
        {
            //Get All Practices
            //Find all records belonging to the sessionId
            var practicesDb = await _db.FindAsync<PracticeDrill>(query => query
                .Include<Drill>(join => join.InnerJoin())
                .Include<Image>(join => join.InnerJoin())
                .Include<Note>(join => join.LeftOuterJoin()));

            return practicesDb.Select(rec =>
                Mapper.Map<PracticeDrillDto>(rec)).ToList();
        }


        #endregion

        #region UPDATE

        public async Task<bool> UpdatePractice(PracticeDto practiceDto)
        {
            //Get the practice by its Id
            var dbRec = new Practice() { Id = practiceDto.Id };
            var practiceDb = await _db.GetAsync(dbRec);

            //store practice Id into a temp var
            var practiceId = practiceDb.Id;

            //Map to the Database object
            practiceDb = Mapper.Map<Practice>(practiceDto);

            //Update ID
            practiceDb.Id = practiceId;

            return await _db.UpdateAsync(practiceDb);

        }

        #endregion


        #region DELETE

        /// <summary>
        /// 
        /// </summary>
        /// <param name="practiceId"></param>
        /// <returns></returns>
        public async Task<bool> DeletePractice(int practiceId)
        {
            //Get the practice by its Id
            var dbRec = new Practice() { Id = practiceId };

            //First delete from the child tables
            //PracticeDrill
            //Find all records belonging to the sessionId
            var practiceDrillsDb = await _db.FindAsync<PracticeDrill>(query => query
                .Where($"{nameof(PracticeDrill.PracticeId):C} = @practiceId")
                .WithParameters(new { practiceId }));

            foreach (var drill in practiceDrillsDb)
            {
                //Delete the the record from the DB
                await _db.DeleteAsync(drill);
            }

            //Now, delete from PracticeDto
            //Delete the the record from the DB
            return await _db.DeleteAsync(dbRec);
        }

        public async Task<bool> DeleteAllSessionPractices(int sessionId)
        {
            bool result = true;

            var practices = await GetSessionPractices(sessionId);

            foreach (var practice in practices)
            {
                result &= await DeletePractice(practice.Id);
            }

            return result;
        }


        #endregion



    }

}