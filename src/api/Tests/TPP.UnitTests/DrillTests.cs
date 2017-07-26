// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

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
    public class DrillTests : IClassFixture<DataInitFixture>
    {
        private readonly ITestOutputHelper _output;

        private DataInitFixture _fixture;
        private DrillService _service;

        public DrillTests(DataInitFixture fixture, ITestOutputHelper output)
        {
            //Initialize the output
            this._output = output;

            //Initialize DbContext in memory
            this._fixture = fixture;

            //Create the test service
            _service = new DrillService(_fixture.Context);

        }

 


        [Fact]
        public async void GetAllDrillsTest()
        {
            try
            {
                var result = await _service.GetAllDrills();
                Assert.NotNull(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }




    }
}