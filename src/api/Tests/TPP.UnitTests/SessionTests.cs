// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using Newtonsoft.Json;
using TPP.Core.Services.Impl;
using TPP.Core.Services.Models;
using Xunit;
using Xunit.Abstractions;

namespace TPP.UnitTests
{
    [CollectionDefinition("TPPDatabase")]
    public class SessionTests : IClassFixture<DataInitFixture>
    {
        private readonly ITestOutputHelper _output;

        private DataInitFixture _fixture;
        private SessionService _service;

        public SessionTests(DataInitFixture fixture, ITestOutputHelper output)
        {
            //Initialize the output
            this._output = output;

            //Initialize DbContext in memory
            this._fixture = fixture;

            //Create the test service
            _service = new SessionService(_fixture.Context);

        }

 

        [Theory]
        [InlineData("10-12-2016")]
        public async void GetSessionsByDateTest(DateTime sessionDate)
        {
            try
            {
                var sessions = await _service.GetSessionsByDate(sessionDate);
                Assert.NotNull(sessions);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (e.Message.StartsWith("Sequence contains no matching element"))
                    Console.WriteLine("Not found");
                throw;
            }

        }



        [Theory]
        [InlineData("10-11-2016", "10-13-2016")]
        public async void GetSessionsByDateRangeTest(DateTime startDate, DateTime endDate)
        {
            try
            {
                var sessions = await _service.GetSessionsRange(startDate, endDate);
                Assert.NotNull(sessions);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (e.Message.StartsWith("Sequence contains no matching element"))
                    Console.WriteLine("Not found");
                throw;
            }

        }


        [Theory]
        [InlineData(1)]
        public async void GetSessionTest(int sessionId)
        {
            try
            {
                var session = await _service.GetSession(sessionId);
                Assert.NotNull(session);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (e.Message.StartsWith("Sequence contains no matching element"))
                    Console.WriteLine("Not found");
                throw;
            }
        }
        [Fact]
        public async void CreateSession()
        {
            var session = new SessionDto
            {
                SessionType = "1",
                Scheduled = DateTime.Now,
                Location = new LocationDto() {Id = 1}
            };

            var result = await _service.CreateSession(session);
            Assert.NotEqual(0, result);
        }

        [Fact]
        public async void GetSessionTypes()
        {
            var result = await _service.GetSessionTypes();
            Assert.NotEqual(null, result);
            Assert.NotEmpty(result);
        }
        [Fact]
        public async void UpdateSession()
        {
            var sessionJson = "{\"sessionType\": \"83\",\"scheduled\": \"2017-08-10T11:00:00\"," +
                "\"location\": {\"name\": \"Stadium\",\"address\": null,\"type\": 0,\"id\": 3}," +
                              "\"users\": [{\"firstName\": \"Roy\",\"lastName\": \"Moran\"," +
                              "\"middleName\": \"N/A\",\"nickname\": \"N/A\",\"nationalityId\": 1," +
                              "\"roleId\": 1,\"gender\": \"Male\",\"height\": 13,\"weight\": 234," +
                              "\"educationId\": 1,\"localeId\": 1,\"dateofBirth\": \"1993-08-26T00:00:00\"," +
                              "\"isActive\": true,\"email\": \"roy28@tppusers.onmicrosoft.com\"," +
                              "\"pathtoPhoto\": \"https://tppapp.blob.core.windows.net/content/photos/1496944106727.png\"," +
                              "\"isEnabled\": true,\"turnedProfessional\": \"2017-06-13T06:48:42.637\"," +
                              "\"fullName\": \"Roy Moran\",\"startDate\": \"2017-06-13T00:00:00\"," +
                              "\"endDate\": \"2017-06-13T00:00:00\",\"amsId\": null,\"aadId\": \"7e398afb-1a13-47c5-9b47-8bdbae9d8e0e\"," +
                              "\"playerInfo\": null,\"teamId\": 0,\"id\": 9912}],\"id\": 161}";
            var session = JsonConvert.DeserializeObject<SessionDto>(sessionJson);
            var result = await _service.UpdateSession(session);
            Assert.NotEqual(false, result);
        }
    }
}