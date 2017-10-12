using System;
using System.Collections.Generic;
using TPP.Core.Data.Entities;
using TPP.Core.Services.Impl;
using TPP.Core.Services.Models;
using Xunit;
using Xunit.Abstractions;

namespace TPP.UnitTests
{
    [CollectionDefinition("TPPDatabase")]
    public class PracticeTests : IClassFixture<DataInitFixture>
    {
        private readonly ITestOutputHelper _output;

        private DataInitFixture _fixture;
        private PracticeService _service;

        public PracticeTests(DataInitFixture fixture, ITestOutputHelper output)
        {
            //Initialize the output
            this._output = output;

            //Initialize DbContext in memory
            this._fixture = fixture;

            //Create the test service
            _service = new PracticeService(_fixture.Context);

        }

 

        [Theory]
        [InlineData(0)] //Unknown - Fail
        public async void GetPracticeTestToFail(int id)
        {
            var practice = await _service.GetPractice(id);
            Assert.NotNull(practice);

            _output.WriteLine($"PracticeDto Name: {practice.Name}");

            //foreach (var drill in practice.Drills)
            //{
            //    _output.WriteLine($"Drill Name: {drill.Name}");
            //}
        }




        [Fact]
        public async void GetAllPracticesTest()
        {
            try
            {
                var practices = await _service.GetAllPractices();
                Assert.NotNull(practices);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }


        [Theory]
        [InlineData(0)] //PracticeId
        public async void GetPracticeTest(int practiceId)
        {
            try
            {
                var result = await _service.GetPractice(practiceId);
                Assert.NotNull(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }


        [Theory]
        [InlineData(1)] //SessionId
        public async void GetPracticeForSessionTest(int sessionId)
        {
            try
            {
                var practices = await _service.GetSessionPractices(sessionId);
                Assert.NotNull(practices);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }


        [Fact]
        public async void GetPracticeDrillsTest()
        {
            try
            {
                var result = await _service.GetPracticeDrills();
                Assert.NotNull(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }



        [Theory]
        [InlineData(1)]
        public async void GetPracticeDrillsTest(int practiceId)
        {
            try
            {
                var result = await _service.GetPracticeDrills(practiceId);
                Assert.NotNull(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }


        [Fact]
        public async void CreatePracticeTest()
        {
            try
            {
                var practice = new PracticeDto()
                {
                    Coach = null,
                    EstimatedTrainingLoad = 525,
                    IsModified = false,
                    Name = $"Test Practice #{new Random().Next(1, 100)}",
                    Note = null,
                    RecommendedTrainingLoad = 375,
                    Session = null,
                    SessionId = 1,
                    Side = null,
                    SubTopic = "Test Subtopic",
                    TeamId = 1,
                    Topic = "Test Topic",
                    PracticeDrills = new List<PracticeDrillDto>() {new PracticeDrillDto()
                    {
                        CalculatedTrainingLoad = 175,
                        Drill = null,
                        DrillId = 2,
                        Duration = 30,
                        IsModified = false,
                        Note = null,
                        NoteId = 0,
                        NumberOfPlayers = 10,
                        Practice = null,
                        PracticeId = 0,
                        Sequence = 1,
                        Size = "60x60"
                    }, new PracticeDrillDto()
                        {
                            CalculatedTrainingLoad = 200,
                            Drill = null,
                            DrillId = 4,
                            Duration = 30,
                            IsModified = false,
                            Note = null,
                            NoteId = 0,
                            NumberOfPlayers = 7,
                            Practice = null,
                            PracticeId = 0,
                            Sequence = 2,
                            Size = "18x18"
                        }}
                };
                var result = await _service.CreatePractice(practice);
                Assert.NotEqual(0, result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }


    }
}