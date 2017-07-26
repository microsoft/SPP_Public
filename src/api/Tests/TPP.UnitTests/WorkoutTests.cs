// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TPP.Core.Services.Impl;
using TPP.Core.Services.Models;
using Xunit;
using Xunit.Abstractions;

namespace TPP.UnitTests
{
    [CollectionDefinition("TPPDatabase")]
    public class WorkoutTests : IClassFixture<DataInitFixture>
    {
        private readonly ITestOutputHelper _output;

        private DataInitFixture _fixture;
        private WorkoutService _service;

        public WorkoutTests(DataInitFixture fixture, ITestOutputHelper output)
        {
            //Initialize the output
            this._output = output;

            //Initialize DbContext in memory
            this._fixture = fixture;

            //Create the test service
            _service = new WorkoutService(_fixture.Context);

        }

 




        [Theory]
        [InlineData(1)] //SessionId
        public async void GetSessionWorkout(int sessionId)
        {
            try
            {
                List<AthleteWorkoutDto> workouts = ((List <AthleteWorkoutDto> )await _service.GetSessionWorkouts(sessionId));
                Assert.NotNull(workouts);
                Assert.NotEqual(false, workouts.Count > 0);

                //Save to the JSon string
                //var athleteWorkout = JsonConvert.SerializeObject(workouts[0]);
                //using (FileStream fs = new FileStream("D:\\workout.json", FileMode.Create))
                //{
                //    StreamWriter writer = new StreamWriter(fs);
                //    await writer.WriteAsync(athleteWorkout);
                //    await writer.FlushAsync();
                //}
            }
            catch (System.Exception ex)
            {

                throw;
            }

        }




        [Fact]
        public async void CreateWorkoutTest()
        {
            try
            {
                var athleteWorkout = new AthleteWorkoutDto()
                {
                    Id = 0,
                    Name = "Test #1 workout",
                    Session = null,
                    SessionId = 1,
                    Topic = "Lower Body",
                    SubTopic = "Body Core",
                    Category = "Strength",
                    Exercises = new List<AthleteExerciseDto>()
                    {
                        new AthleteExerciseDto()
                        {
                            Id = 1,
                            Category = "Strength",
                            Description = "Work on legs.",
                            Duration = 90,
                            ImageUrl = "http://exercises.youtrain.me.s3.amazonaws.com/wp-content/uploads/Dumbbell-Standing-Step-Up-622x485.png",
                            IsDone = false,
                            IsModified = false,
                            Name = "Bench Press",
                            Note = new NoteDto() {
                                Id = 0,
                                Text = "Rest 10 seconds",
                                Created = DateTime.Parse("2017-03-03")
                            },
                            TrainingLoad = "100",
                            Sets = new AthleteExerciseSetDto()
                            {
                                Id = 0,
                                Order = 1,
                                RecoveryTimeInMin = 20,
                                Reps = 15,
                                Sets = 4,
                                Weight = 100
                            }
                        }
                    }
                };

                var result = await _service.CreateWorkout(athleteWorkout);
                Assert.NotEqual(false, result);

            }
            catch (System.Exception ex)
            {

                throw;
            }

        }







        [Fact]
        public async void UpdateWorkoutExerciseTest()
        {
            try
            {
                var athleteWorkout = new AthleteWorkoutDto()
                {
                    Id = 1,
                    Name = "Hazard's workout",
                    Session = new SessionDto(),
                    SessionId = 1,
                    Topic = "Upper Body",
                    SubTopic = "Body Core",
                    Category = "Strength",
                    Exercises = new List<AthleteExerciseDto>()
                    {
                        new AthleteExerciseDto()
                        {
                            Id    = 0,
                            Category = "Upper Body",
                            Description = "When doing a bench press, take 3 seconds to drop from start position to a place around 4 inches away from your chest. Hold for a second before exploding back to start position as quickly as possible.",
                            Duration = 0,
                            ImageUrl = "http://jayantpandey.com/jayantpandey/ExerciseContent/img/flatbenchpress.jpg",
                            IsDone = true,
                            IsModified = false,
                            Name = "Bench Press",
                            Note = new NoteDto() {
                                Id = 1,
                                Text = "Rest 10 seconds and drink more liquids",
                                Created = DateTime.Parse("2016-10-07")
                            },
                            TrainingLoad = "195",
                            Sets = new AthleteExerciseSetDto()
                            {
                                Id = 0,
                                Order = 1,
                                RecoveryTimeInMin = 25,
                                Reps = 12,
                                Sets = 3,
                                Weight = 140
                            }
                        },
                        new AthleteExerciseDto()
                        {
                            Id = 1,
                            Category = "Upper Body",
                            Description = "Work on Arms Strength",
                            Duration = 0,
                            ImageUrl = "https://bretcontreras.com/wp-content/uploads/Seated-Barbell-Overhead-Press.jpg",
                            IsDone = false,
                            IsModified = true,
                            Name = "Military Press",
                            Note = new NoteDto() {
                                Id = 2,
                                Text = "Do 3 sets",
                                Created = DateTime.Parse("2016-10-12")
                            },
                            TrainingLoad = "95",
                            Sets = new AthleteExerciseSetDto()
                            {
                                Id = 1,
                                Order = 2,
                                RecoveryTimeInMin = 30,
                                Reps = 6,
                                Sets = 3,
                                Weight = 80
                            }
                        }
                    }
                };


                var result = await _service.UpdateWorkoutExercises(athleteWorkout);
                Assert.NotEqual(false, result);

            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

        }


        [Fact]
        public async void GetExercisesTest()
        {
            try
            {
                var exercises = await _service.GetAllExercises();
                Assert.NotNull(exercises);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }




        [Fact]
        public async void GetWorkoutsTest()
        {
            try
            {
                var workouts = await _service.GetWorkouts();
                Assert.NotNull(workouts);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }


    }
}