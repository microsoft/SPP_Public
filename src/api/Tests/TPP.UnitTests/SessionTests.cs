// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
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

    }
}