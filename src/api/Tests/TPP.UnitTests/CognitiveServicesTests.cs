// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.Collections.Generic;
using System.Threading.Tasks;
using TPP.Core.Services.Impl;
using TPP.Core.Services.Models;
using Xunit;
using Xunit.Abstractions;

namespace TPP.UnitTests
{
    [CollectionDefinition("TPPDatabase")]
    public class CognitiveServicesTests : IClassFixture<DataInitFixture>
    {
        private readonly ITestOutputHelper _output;

        private DataInitFixture _fixture;
        private CognitiveService _service;

        public CognitiveServicesTests(DataInitFixture fixture, ITestOutputHelper output)
        {
            //Initialize the output
            this._output = output;

            //Initialize DbContext in memory
            this._fixture = fixture;

            //Create the test service
            _service = new CognitiveService(_fixture.Context);

        }




        [Fact]
        public async void CreateCSKeys()
        {
            var keys = new CognitiveServiceKeysDto()
            {
                WorkspaceKey = "b798ac78-acf5-44e9-a405-46a85736fd8e",
                FaceApiKey = "73a191f1899e44ff9f14be1c81e9e7b7",
                EmotionApiKey = "837be46e535547ec8892f7fdf5cf1645"
            };

            var result = await _service.CreateCognitiveServiceKeys(keys);
            Assert.Equal(true, result);
        }


        [Theory]
        [InlineData(1)]
        public async Task<CognitiveServiceKeysDto> GetKeys(int teamId)
        {
            var keys = await _service.GetCognitiveServiceKeys(teamId);
            Assert.NotNull(keys);
            return keys;
        }



        [Theory]
        [InlineData("Danny Garber")]
        public async void AuthenticateUser(string fullName)
        {
            var teamId = await _service.AuthenticateUserByFace(new List<UserIdentityDto>()
            {
                new UserIdentityDto()
                {
                    FullName = fullName,
                    Confidence = 0.8,
                    Emotions = new EmotionsDto()
                    {
                        Neutral = 1,
                        Anger = 0,
                        Disgust = 0,
                        Fear = 0,
                        Contempt = 0,
                        Happiness = 0,
                        Sadness = 0,
                        Surprise = 0
                    }
                }
            });
            Assert.NotEqual(0, teamId);
        }


        [Theory]
        [InlineData("Non-Existant User")]
        public async void FailToAuthenticateUser(string fullName)
        {
            var teamId = await _service.AuthenticateUserByFace(new List<UserIdentityDto>()
            {
                new UserIdentityDto()
                {
                    FullName = fullName,
                    Confidence = 0.8,
                    Emotions = new EmotionsDto()
                    {
                        Neutral = 1,
                        Anger = 0,
                        Disgust = 0,
                        Fear = 0,
                        Contempt = 0,
                        Happiness = 0,
                        Sadness = 0,
                        Surprise = 0
                    }
                }
            });
            Assert.Equal(0, teamId);
        }


        [Theory]
        [InlineData(2)]
        public async void UpdateKeys(int keyId)
        {
            var keys = await GetKeys(1);
            keys.CameraName = "Sony";
            keys.MinDetectableFaceCoveragePercentage = 7;

            var result = await _service.UpdateCognitiveServiceKeys(keys);
            Assert.Equal(true, result);
        }


        [Theory]
        [InlineData(2)]
        public async void DeleteKeys(int keyId)
        {
            var result = await _service.DeleteCognitiveServiceKeys(keyId);
            Assert.Equal(true, result);
        }

    }
}