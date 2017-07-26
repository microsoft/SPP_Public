// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
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
    public class WorkoutProfile : Profile
    {
        public WorkoutProfile()
        {
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


            CreateMap<WorkoutData, AthleteWorkoutDto>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Workout.Name))
               .ForMember(dest => dest.Topic, opt => opt.MapFrom(src => src.Workout.Topic))
               .ForMember(dest => dest.SubTopic, opt => opt.MapFrom(src => src.Workout.SubTopic))
               .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Workout.Category))
               ;


            CreateMap<Note, NoteDto>();
            CreateMap<NoteDto, Note>();

            CreateMap<WorkoutExerciseData, AthleteExerciseDto>()
               .ForMember(dest => dest.ExerciseDataId, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.WorkoutExercise.Exercise.Name))
               .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.WorkoutExercise.Exercise.Category))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.WorkoutExercise.Exercise.Description))
               .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.WorkoutExercise.Exercise.Image.Url))
               .ForMember(dest => dest.IsDone, opt => opt.UseValue(false))
               .ForMember(dest => dest.IsModified, opt => opt.MapFrom(src => src.isModified))
               .ForMember(dest => dest.Duration, opt => opt.ResolveUsing<DurationResolver>())
               .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.Note))
               .ForMember(dest => dest.TrainingLoad, opt => opt.ResolveUsing<TrainingLoadResolver>())
               .ForMember(dest => dest.Sets, opt => opt.ResolveUsing<SetResolver>())
               ;

            CreateMap<WorkoutExerciseData, AthleteExerciseSetDto>()
               .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.WorkoutExercise.SequenceNumber))
               ;


            //Insert maps
            CreateMap<AthleteWorkoutDto, Workout>();
            CreateMap<AthleteWorkoutDto, WorkoutData>();

            CreateMap<AthleteExerciseDto, Image>()
               .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.ImageUrl))
               ;

            CreateMap<AthleteExerciseDto, Exercise>();
            CreateMap<Exercise, AthleteExerciseSetDto>();

            CreateMap<AthleteExerciseDto, WorkoutExercise>()
               .ForMember(dest => dest.ExerciseId, opt => opt.MapFrom(src => src.Id))
               ;

            CreateMap<WorkoutExercise, ExerciseDto>()
               .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Exercise.Category))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Exercise.Description))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Exercise.Name))
               .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Exercise.Image))
               ;

            CreateMap<AthleteExerciseDto, WorkoutExerciseData>()
               .ForMember(dest => dest.WorkoutId, opt => opt.Ignore())
               .ForMember(dest => dest.WorkoutExerciseId, opt => opt.Ignore())
               .ForMember(dest => dest.MessageId, opt => opt.Ignore())
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ExerciseDataId))
               .ForMember(dest => dest.Sets, opt => opt.MapFrom(src => src.Sets.Sets))
               .ForMember(dest => dest.Reps, opt => opt.MapFrom(src => src.Sets.Reps))
               .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Sets.Weight))
               .ForMember(dest => dest.RecoveryTimeInMin, opt => opt.MapFrom(src => src.Sets.RecoveryTimeInMin))
               .ForMember(dest => dest.isCompleted, opt => opt.MapFrom(src => src.IsDone))
               .ForMember(dest => dest.NoteId, opt => opt.MapFrom(src => src.Note.Id))
               ;

        }


    }


    public class TrainingLoadResolver : IValueResolver<WorkoutExerciseData, AthleteExerciseDto, string>
    {
        public string Resolve(WorkoutExerciseData source, AthleteExerciseDto destination, string destMember, ResolutionContext context)
        {
            var weight = source.Weight ?? 0;
            var reps = source.Reps ?? 0;

            var tl = (int)(weight * (1 + (0.033 * reps)));
            return tl.ToString();
        }
    }


    public class DurationResolver : IValueResolver<WorkoutExerciseData, AthleteExerciseDto, int>
    {
        public int Resolve(WorkoutExerciseData source, AthleteExerciseDto destination, int destMember, ResolutionContext context)
        {
            var sets = source.Sets ?? 0;
            var reps = source.Reps ?? 0;
            var repDuration = 2 / 60; //in minutes
            var recoveryTime = source.RecoveryTimeInMin ?? 0;

            return sets * reps * repDuration + ((sets > 1) ? recoveryTime * (sets - 1) : 0);
        }
    }



    public class SetResolver : IValueResolver<WorkoutExerciseData, AthleteExerciseDto, AthleteExerciseSetDto>
    {
        public AthleteExerciseSetDto Resolve(WorkoutExerciseData source, AthleteExerciseDto destination,
            AthleteExerciseSetDto destMember, ResolutionContext context)
        {
            return new AthleteExerciseSetDto()
            {
                Order = source.WorkoutExercise.SequenceNumber,
                Sets = source.Sets ?? 0,
                Reps = source.Reps ?? 0,
                RecoveryTimeInMin = source.RecoveryTimeInMin ?? 0,
                Weight = source.Weight ?? 0F,
                TrainingLoad = (int) ((source.Weight ?? 0F) * (1 + (0.033 * (source.Reps ?? 0))))
            };
        }
    }


    public class WorkoutService : TPPDbService, IWorkoutService
    {
        private IDbConnection _db;

        public WorkoutService(TPPContext context) : base(context)
        {
            _db = context?.TppDbConnection;
            RegisterEntities();
        }

        public WorkoutService(BaseDbContext context) : base(context)
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
        /// <returns></returns>
        public async Task<IList<AthleteWorkoutDto>> GetWorkouts()
        {
            var athleteWorkoutCol = new List<AthleteWorkoutDto>();
            //Get all workout data
            var workoutDataDb = await _db.FindAsync<WorkoutData>(query => query
                .Include<Workout>(join => join.InnerJoin())
                .Include<Note>(join => join.InnerJoin()));

            foreach (var workout in workoutDataDb)
            {
                //Initialize the workout info
                var athleteWorkout = Mapper.Map<AthleteWorkoutDto>(workout);
                athleteWorkout.Exercises = new List<AthleteExerciseDto>();

                //Get the workout exercise data
                var exerciseData = await _db.FindAsync<WorkoutExerciseData>(query => query
                    .Where($"{nameof(WorkoutExerciseData.WorkoutId):C} = @workoutId")
                    .Include<Workout>(join => @join.InnerJoin())
                    .Include<WorkoutExercise>(join => @join.InnerJoin())
                    .Include<Exercise>(join => join.InnerJoin())
                    .Include<Image>(join => join.InnerJoin())
                    .Include<Message>(join => @join.InnerJoin())
                    .Include<Note>(join => @join.InnerJoin())
                    .WithParameters(new { workoutId = workout.WorkoutId }));

                foreach (var exercise in exerciseData)
                {
                    //Map Exercise Data
                    var atExercise = Mapper.Map<AthleteExerciseDto>(exercise);
                    atExercise.Sets = Mapper.Map<AthleteExerciseSetDto>(exercise);

                    //add to the workout collection
                    athleteWorkout.Exercises.Add(atExercise);
                }

                athleteWorkoutCol.Add(athleteWorkout);
            }


            return athleteWorkoutCol;

        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public async Task<IList<AthleteWorkoutDto>> GetSessionWorkouts(int sessionId)
        {
            var athleteWorkoutCol = new List<AthleteWorkoutDto>();
            //Get the workout data
            var workoutDataDb = await _db.FindAsync<WorkoutData>(query => query
                .Where($"{nameof(WorkoutData.SessionId):C} = @sessionId")
                .Include<Workout>(join => join.InnerJoin())
                .Include<Note>(join => join.LeftOuterJoin())
                .Include<Session>(join => join.InnerJoin())
                .WithParameters(new { sessionId }));

            foreach (var workout in workoutDataDb)
            {
                //Initialize the workout info
                var athleteWorkout = Mapper.Map<AthleteWorkoutDto>(workout);
                athleteWorkout.Id = workout.Workout.Id;
                athleteWorkout.SessionId = workout.Session.Id;
                athleteWorkout.Exercises = new List<AthleteExerciseDto>();

                //Get the workout exercise data
                var exerciseData = await _db.FindAsync<WorkoutExerciseData>(query => query
                    .Where($"{nameof(WorkoutExerciseData.WorkoutId):C} = @workoutId")
                    .Include<Workout>(join => @join.InnerJoin())
                    .Include<WorkoutExercise>(join => @join.InnerJoin())
                    .Include<Exercise>(join => join.InnerJoin())
                    .Include<Image>(join => join.InnerJoin())
                    .Include<Message>(join => @join.LeftOuterJoin())
                    .Include<Note>(join => @join.LeftOuterJoin())
                    .WithParameters(new { workoutId = workout.WorkoutId }));

                foreach (var exercise in exerciseData)
                {
                    //Map Exercise Data
                    var atExercise = Mapper.Map<AthleteExerciseDto>(exercise);
                    atExercise.Sets = Mapper.Map<AthleteExerciseSetDto>(exercise);

                    //add to the workout collection
                    athleteWorkout.Exercises.Add(atExercise);
                }

                athleteWorkoutCol.Add(athleteWorkout);
            }


            return athleteWorkoutCol;

        }





        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IList<AthleteExerciseDto>> GetAllExercises()
        {
            //Get All Exercises
            var exercises = await _db.FindAsync<WorkoutExerciseData>(query => query
                .Include<WorkoutExercise>(join => join.InnerJoin())
                .Include<Exercise>(join => join.InnerJoin())
                .Include<Image>(join => join.LeftOuterJoin())
                .Include<Note>(join => join.LeftOuterJoin())
                );

            return exercises.Select(data => Mapper.Map<AthleteExerciseDto>(data)).ToList();

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="workoutDto"></param>
        /// <returns></returns>
        public async Task<bool> CreateWorkout(AthleteWorkoutDto workoutDto)
        {
            bool result = true;

            //1. Create Workout DB Record
            var workoutDb = Mapper.Map<Workout>(workoutDto);
            await _db.InsertAsync(workoutDb);
            result &= workoutDb.Id > 0;

            //2. Create a WorkoutData DB Record
            var workoutDataDb = Mapper.Map<WorkoutData>(workoutDto);
            workoutDataDb.WorkoutId = workoutDb.Id;
            workoutDataDb.Duration = workoutDto.Exercises.Sum(x => x.Duration);
            await _db.InsertAsync(workoutDataDb);
            result &= workoutDataDb.Id > 0;

            int sequenceOrder = 1;
            foreach (var exercise in workoutDto.Exercises)
            {
                //3. Create a Note DB Record
                var noteDb = new Note()
                {
                    Text = exercise.Note.Text,
                    Created = DateTime.UtcNow
                };
                await _db.InsertAsync(noteDb);
                result &= noteDb.Id > 0;

                //4. Create a Imagete DB Record
                //Update a Image DB Record by creating a new one and updating the exercise's Image ID, but only if the URL has changed
                var imageRec = await _db.FindAsync<Image>(query => query
                    .Where($"{nameof(Image.Url):C} = @imageUrl")
                    .WithParameters(new { imageUrl = exercise.ImageUrl }));
                var imageId = (imageRec != null && imageRec.Any()) ? imageRec.First().Id : 0;
                if (imageId == 0)
                {
                    var imgDb = new Image() { Url = exercise.ImageUrl };
                    await _db.InsertAsync(imgDb);
                    result &= imgDb.Id > 0;
                    imageId = imgDb.Id;
                }

                //5. Create Exercise DB Record
                var exercDb = Mapper.Map<Exercise>(exercise);
                exercDb.ImageId = imageId;
                await _db.InsertAsync(exercDb);
                result &= exercDb.Id > 0;

                //6. Create WorkoutExercise DB Record
                var workoutExeDb = Mapper.Map<WorkoutExercise>(exercise);
                workoutExeDb.SequenceNumber = sequenceOrder;
                await _db.InsertAsync(workoutExeDb);
                result &= workoutExeDb.Id > 0;

                //7. Create WorkoutExerciseData DB Record
                var workoutExeDataDb = Mapper.Map<WorkoutExerciseData>(exercise);
                workoutExeDataDb.WorkoutId = workoutDb.Id;
                workoutExeDataDb.WorkoutExerciseId = workoutExeDb.Id;
                workoutExeDataDb.NoteId = noteDb.Id;
                await _db.InsertAsync(workoutExeDataDb);
                result &= workoutExeDataDb.Id > 0;

                sequenceOrder++;
            }


            return result;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="workoutDto"></param>
        /// <returns></returns>
        public async Task<bool> UpdateWorkout(AthleteWorkoutDto workoutDto)
        {
            //1. Update the Workout DB Record
            var workoutDb = Mapper.Map<Workout>(workoutDto);
            return await _db.UpdateAsync(workoutDb);
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="workoutDto"></param>
        /// <returns></returns>
        public async Task<bool> UpdateWorkoutExercises(AthleteWorkoutDto workoutDto)
        {
            bool result = true;

            //1. Update the Workout Exercises DB Record
            foreach (var exercise in workoutDto.Exercises)
            {
                //Retrieve the Exercise entity from DB
                var exerciseRec = await _db.FindAsync<WorkoutExercise>(query => query
                    .Include<Exercise>(join => join.InnerJoin()
                        .Where($"{nameof(Exercise.Id):C} = @exerciseId"))
                    .Include<Image>(join => join.LeftOuterJoin())
                    .WithParameters(new { exerciseId = exercise.Id }));
                if (exerciseRec == null) continue;
                var exerciseDb = exerciseRec.First();

                //3. Update a Note DB Record
                var noteDb = Mapper.Map<Note>(exercise.Note);
                result &= await _db.UpdateAsync(noteDb);

                //4. Update Exercise DB Record
                var exercDb = Mapper.Map<Exercise>(exercise);
                //Update a Image DB Record by creating a new one and updating the exercise's Image ID, but only if the URL has changed
                if (exerciseDb.Exercise.Image.Url != exercise.ImageUrl)
                {
                    var imgDb = new Image() { Url = exercise.ImageUrl };
                    await _db.InsertAsync(imgDb);
                    exercDb.ImageId = imgDb.Id;
                }
                else
                {
                    exercDb.ImageId = exerciseDb.Exercise.Image.Id;
                }
                result &= await _db.UpdateAsync(exercDb);

                //6. Update WorkoutExercise DB Record
                //var workoutExeDb = Mapper.Map<WorkoutExercise>(workoutDto.);
                //result &= await _db.UpdateAsync(workoutExeDb);

                //7. Update WorkoutExerciseData DB Record
                var workoutExeDataDb = Mapper.Map<WorkoutExerciseData>(exercise);
                workoutExeDataDb.WorkoutId = workoutDto.Id;
                workoutExeDataDb.WorkoutExerciseId = exerciseDb.Id;
                //Get Message Id
                var messageRec = await _db.FindAsync<WorkoutExerciseData>(query => query
                    .Where($"{nameof(WorkoutExerciseData.Id):C} = @exerciseDataId")
                    .Include<Message>(join => join.LeftOuterJoin())
                    .WithParameters(new { exerciseDataId = exercise.ExerciseDataId }));
                if (messageRec == null) continue;
                var messageDb = messageRec.First();
                workoutExeDataDb.MessageId = messageDb.Message.Id;
                result &= await _db.UpdateAsync(workoutExeDataDb);
            }


            return result;
        }



        public async Task<bool> DeleteWorkout(int workoutId)
        {
            //Get the workout by its Id
            var workoutDb = await _db.GetAsync(new Workout() { Id = workoutId });

            //Remove all workout data for the given workout
            var workoutDataDb = await _db.FindAsync<WorkoutData>(query => query
                .Where($"{nameof(WorkoutData.WorkoutId):C} = @workoutId")
                .WithParameters(new { workoutId }));
            await _db.DeleteAsync(workoutDataDb);


            //Delete the workout record from the DB
            return await _db.DeleteAsync(workoutDb);

        }

        public async Task<bool> DeleteAllSessionWorkouts(int sessionId)
        {
            bool result = true;

            //Get the workout data
            var workoutDataDb = await _db.FindAsync<WorkoutData>(query => query
                .Where($"{nameof(WorkoutData.SessionId):C} = @sessionId")
                .Include<Workout>(join => join.InnerJoin())
                .WithParameters(new { sessionId }));

            //Remove all workout data for the given workout
            result &= await _db.DeleteAsync(workoutDataDb);

            foreach (var workoutData in workoutDataDb)
            {
                //Delete the workout record from the DB
                result &= await _db.DeleteAsync(workoutData.Workout);
            }

            return result;
        }

    }
}