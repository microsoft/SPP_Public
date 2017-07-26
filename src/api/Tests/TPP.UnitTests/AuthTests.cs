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
    public class AuthTests : IClassFixture<DataInitFixture>
    {
        private readonly ITestOutputHelper _output;

        private DataInitFixture _fixture;
        private AuthService _service;

        public AuthTests(DataInitFixture fixture, ITestOutputHelper output)
        {
            //Initialize the output
            this._output = output;

            //Initialize DbContext in memory
            this._fixture = fixture;

            //Create the test service
            _service = new AuthService(_fixture.Context);

        }



        [Theory]
        [InlineData("0caf9eb2-3faf-4ab2-9cbb-86b1df945a62")] //Danny G. - Success
        public async void AuthenticateUser(string userId)
        {
            var teamId = await _service.AuthenticateUser(userId);
            Assert.NotNull(teamId);

            if (teamId > 0)
            {
                _output.WriteLine($"UserDto with AAD Id={userId} is authenticated. The TeamDto Id={teamId}");
            }
            else
            {
                _output.WriteLine($"UserDto with AAD Id={userId} is not authenticated.");
                Assert.NotEqual(0, teamId);
            }
        }


        [Theory]
        [InlineData("B99EF498-2B14-43D7-9AC5-9B09D174DE6B")] //Unknown - Fail
        public async void FailAuthenticateUser(string userId)
        {
            var exception = await Assert.ThrowsAsync<TPPApiException>(async () =>
            {
                await _service.AuthenticateUser(userId);
            });

            Assert.Equal(typeof(TPPApiException), exception.GetType());
        }


        [Theory]
        [InlineData(1)] //Session Id
        public async void CreateUser(int sessionId)
        {
            var newUser = new UserDto()
            {
                FirstName = "Lionel",
                LastName = "Messi",
                FullName = "Lionel Messi",
                RoleId = 2,
                Height = 170,
                isEnabled = true,
                isActive = true,
                TeamId = 1,
                Email = "messi@tppusers.onmicrosoft.com",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddYears(1),
                PlayerInfo = new PlayerDto
                {
                    IsInjured = false,
                    IsKeeper = "No",
                    Availability = "2017",
                    DominantSkillId = 1,
                    IsResting = true,
                    JerseyNumber = 10,
                    PositionId = 6,
                    SubPositionId = 2
                }
            };

            var userId = await _service.CreateUser(newUser);
            Assert.NotEqual(0, userId);

            //Create the session service
            var sessionSvc = new SessionService(this._fixture.Context);
            await sessionSvc.AddUserToSession(sessionId, userId);
        }


        [Theory]
        [InlineData(9915)] //Test user object Id
        public async void DeleteUser(int userId)
        {

            var result = await _service.DeleteUser(userId);
            Assert.NotEqual(0, result);

            //Create the session service
            var sessionSvc = new SessionService(this._fixture.Context);
            await sessionSvc.RemoveUserFromSession(1, userId);

        }





        [Theory]
        [InlineData(9718)]
        public async void GetUser(int userId)
        {
            var user = await _service.GetUser(userId);
            Assert.NotNull(user);
        }


        [Theory]
        [InlineData(100000)]
        public async void GetNonExistingUser(int userId)
        {
            var user = await _service.GetUser(userId);
            Assert.Null(user);
        }


        [Fact]
        public async void UpdateUser()
        {
            var user = new UserDto()
            {
                Id = 9718,
                FirstName = "Lionel",
                LastName = "Messi",
                MiddleName = "Andres",
                Nickname = "Leo",
                Height = 169,
                isEnabled = true,
                isActive = true,
                TeamId = 1
            };

            var result = await _service.UpdateUser(user);
            Assert.Equal(true, result);
        }



        [Fact]
        public async void GetTokenTest()
        {
            var appInfo = new AppCredentialsDTO()
            {
                AADInstance = "https://login.microsoftonline.com/{0}",
                Audience = "https://tppadmin.onmicrosoft.com/tppapi",
                Tenant = "tppadmin.onmicrosoft.com",
                ClientId = "db2983b0-7da1-45a5-a9f8-53c0a76e4ab9",
                ClientKey = "r8HkjB0BmVnc3D3JWCpzLbVQx1rX3Dxm3lsbNktnOlg="
            };

            var appCred = await _service.GetToken(appInfo);
            Assert.NotNull(appCred.Token);
        }

    }
}