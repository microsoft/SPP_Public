// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using TPP.Core.Services.Impl;
using TPP.Core.Services.Models;
using Xunit;
using Xunit.Abstractions;

namespace TPP.UnitTests
{
    [CollectionDefinition("TPPDatabase")]
    public class QuestionnaireTests : IClassFixture<DataInitFixture>
    {
        private readonly ITestOutputHelper _output;

        private DataInitFixture _fixture;
        private QuestionnaireService _service;
        private PlayerResponseService _playerresponseservice;

        public QuestionnaireTests(DataInitFixture fixture, ITestOutputHelper output)
        {
            //Initialize the output
            this._output = output;

            //Initialize DbContext in memory
            this._fixture = fixture;

            //Create the test service
            _service = new QuestionnaireService(_fixture.Context);
            _playerresponseservice = new PlayerResponseService(_fixture.Context);

        }



        [Theory]
        [InlineData(1)]
        public async void GetSessionQuestionnairesTest(int sessionId)
        {
            try
            {
                var questions = await _service.GetSessionQuestionnaires(sessionId);
                Assert.NotNull(questions);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (e.Message.StartsWith("Sequence contains no matching element"))
                    Console.WriteLine(e.Message);
                throw;
            }
        }

        [Theory]
        [InlineData(333333, 1)]
        public async void GetQuestionnairePlayerResponseTest(int playerId, int questionnaireId)
        {
            try
            {
                var responses = await _playerresponseservice.GetQuestionnairePlayerResponse(playerId,questionnaireId);
                Assert.NotNull(responses);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (e.Message.StartsWith("Sequence contains no matching element"))
                    Console.WriteLine(e.Message);
                throw;
            }
        }

        [Theory]
        [InlineData(1,3)]
        public async void GetPlayerResponseTest(int sessionId, int playerId)
        {
            try
            {
                var responses = await _service.GetPlayerResponse(sessionId, playerId);
                Assert.NotNull(responses);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (e.Message.StartsWith("Sequence contains no matching element"))
                    Console.WriteLine(e.Message);
                throw;
            }
        }



        [Fact]
        public async void CreateQuestionnaireTest()
        {
            try
            {
                var questionnaire = new AthleteQuestionnaireDto()
                {
                    IsEnabled = true,
                    Name = $"Test Questionnaire Name #{new Random().Next(1, 100)}",
                    SessionId = 1,
                    SequenceOrder = 1,
                    StartDateTime = DateTime.UtcNow,
                    EndDateTime = DateTime.UtcNow.AddYears(1),
                    Questions = new List<AthleteQuestionDto>()
                    {
                        new AthleteQuestionDto()
                        {
                            Text = $"Test Question #{new Random().Next(1, 100)}",
                            SequenceOrder = 1,
                            MinCaptionValue = new KeyValuePair<string, int>("Low", 0),
                            MidCaptionValue = new KeyValuePair<string, int>("Mid", 5),
                            MaxCaptionValue = new KeyValuePair<string, int>("Max", 10),
                        },
                        new AthleteQuestionDto()
                        {
                            Text = $"Test Question #{new Random().Next(1, 100)}",
                            SequenceOrder = 2,
                            MinCaptionValue = new KeyValuePair<string, int>("Sad", 0),
                            MidCaptionValue = new KeyValuePair<string, int>("Normal", 5),
                            MaxCaptionValue = new KeyValuePair<string, int>("Happy", 10),
                        }
                    }
                };

                var responses = await _service.CreateQuestionnaire(questionnaire);
                Assert.NotNull(responses);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (e.Message.StartsWith("Sequence contains no matching element"))
                    Console.WriteLine(e.Message);
                throw;
            }
        }


    }
}