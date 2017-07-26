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
    public class TeamTests : IClassFixture<DataInitFixture>
    {
        private readonly ITestOutputHelper _output;

        private DataInitFixture _fixture;
        private TeamService _service;

        public TeamTests(DataInitFixture fixture, ITestOutputHelper output)
        {
            //Initialize the output
            this._output = output;

            //Initialize DbContext in memory
            this._fixture = fixture;

            //Create the test service
            _service = new TeamService(_fixture.Context);

        }



        [Fact]
        public async void GetAllPlayers()
        {
            try
            {
                var team = await _service.GetAllTeamPlayers();
                Assert.NotNull(team);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [Theory]
        [InlineData(1)]
        public async void GetTeamPlayers(int teamId)
        {
            try
            {
                var team = await _service.GetDomesticTeamPlayers(teamId);
                Assert.NotNull(team);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        [Theory]
        [InlineData(1)]
        public async void GetTeamImages(int teamId)
        {
            try
            {
                var teamImages = await _service.GetTeamImages(teamId);
                Assert.NotNull(teamImages);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        [Fact]
        public async void AddTeamImages()
        {
            try
            {
                var teamImagesDto = new TeamImagesDto()
                {
                    TeamId = 1,
                    DateCreated = DateTime.UtcNow,
                    IsActive = true,
                    Images =
                        new List<ImageDto>()
                        {
                            new ImageDto() {Url = "https://tppapp.blob.core.windows.net/content/Images/Edit_BG.jpg"},
                            new ImageDto() {Url = "https://tppapp.blob.core.windows.net/content/Images/ChelseaFC_Logo.png"}
                        }
                };
                var result = await _service.AddTeamImages(teamImagesDto);
                Assert.Equal(true, result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }



        [Fact]
        public async void GetAllTeams()
        {
            try
            {
                var team = await _service.GetTeams();
                Assert.NotNull(team);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        [Theory]
        [InlineData(1)]
        public async void GetTeam(int teamId)
        {
            try
            {
                var team = await _service.GetTeam(teamId);
                Assert.NotNull(team);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }



        [Theory]
        [InlineData("Uknown")]
        public async void GetNoTeamByName(string teamName)
        {
            try
            {
                var team = await _service.GetTeamByName(teamName);
                Assert.Null(team);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }



        [Theory]
        [InlineData("Seattle Flames")]
        public async void GetTeamByName(string teamName)
        {
            try
            {
                var team = await _service.GetTeamByName(teamName);
                Assert.NotNull(team);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}