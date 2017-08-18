using TPP.Core.Services.Impl;
using TPP.Core.Services.Models;
using Xunit;
using Xunit.Abstractions;

namespace TPP.UnitTests
{
    [CollectionDefinition("TPPDatabase")]
    public class LocationTests : IClassFixture<DataInitFixture>
    {
        private readonly ITestOutputHelper _output;

        private DataInitFixture _fixture;
        private LocationService _service;

        public LocationTests(DataInitFixture fixture, ITestOutputHelper output)
        {
            //Initialize the output
            this._output = output;

            //Initialize DbContext in memory
            this._fixture = fixture;

            //Create the test service
            _service = new LocationService(_fixture.Context);

        }




        [Fact]
        public async void CreateLocation()
        {
            var newLocation = new LocationDto()
            {
                Name = "Practice Field"
            };

            var result = await _service.CreateLocation(newLocation);
            Assert.NotEqual(0, result);
        }


        [Fact]
        public async void GetLocations()
        {
            var locations = await _service.GetLocations();
            Assert.NotNull(locations);
        }



    }
}