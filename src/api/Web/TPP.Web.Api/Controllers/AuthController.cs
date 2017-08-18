// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;
using TPP.Api.Configuration;
using TPP.Core.Services.Models;

namespace TPP.Api.Controllers.V1
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    
    using Serilog;
    using Serilog.Events;
    
    using TPP.Core.Data;
    using TPP.Core.Services.Impl;

    [Route(ApiRoutes.AuthRoute)]
    public class AuthController : TPPBaseController
    {
        private readonly AuthService _service;

        public AuthController(TPPContext context) : base(context)
        {
            _service = new AuthService(context);
        }


        //GET api/v2/auth/0caf9eb2-3faf-4ab2-9cbb-86b1df945a62
        /// <summary>
        /// Authenticates the user against SQL Database Users table using its Azure AD Object Id
        /// </summary>
        /// <param name="aadId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{aadId}")]
        public async Task<IActionResult> AuthenticateUser(string aadId)
        {
            try
            {
                var result = await _service.AuthenticateUser(aadId);

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves the user entity by its User Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        //GET api/v2/auth/user/1
        [Authorize]
        [HttpGet("user/{userId:int}")]
        public async Task<IActionResult> Get(int userId)
        {
            try
            {
                var result = await _service.GetUser(userId);

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }


        // POST api/v2/auth/user
        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns>true, if successfull; otherwise - false</returns>
        /// <response code="201">true, if successfull; otherwise - false</response>
        /// <response code="400">If the object is null</response>
        [Authorize]
        [HttpPost, Route("user")]
        [ProducesResponseType(typeof(UserDto), 201)]
        [ProducesResponseType(typeof(UserDto), 400)]
        public async Task<IActionResult> CreateUser([FromBody, Required] UserDto userDto)
        {
            try
            {
                if (userDto == null)
                    return BadRequest();

                var result = await _service.CreateUser(userDto);

                return CreatedAtRoute("default", result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }



        // POST api/v2/auth/user/update
        /// <summary>
        /// Updates the existing user
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns>true, if successfull; otherwise - false</returns>
        /// <response code="201">true, if successfull; otherwise - false</response>
        /// <response code="400">If the object is null</response>
        [Authorize]
        [HttpPost, Route("user/update")]
        [ProducesResponseType(typeof(UserDto), 201)]
        [ProducesResponseType(typeof(UserDto), 400)]
        public async Task<IActionResult> UpdateUser([FromBody, Required] UserDto userDto)
        {
            try
            {
                if (userDto == null)
                    return BadRequest();

                var result = await _service.UpdateUser(userDto);

                return CreatedAtRoute("default", result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }




        // POST api/v2/auth/user/delete
        /// <summary>
        /// Delete the user with the specified UserId
        /// </summary>
        /// <param name="user"></param>
        /// <returns>true, if successfull; otherwise - false</returns>
        /// <response code="201">true, if successfull; otherwise - false</response>
        /// <response code="400">If the object is null</response>
        [Authorize]
        [HttpPost, Route("user/delete/{userId:int}")]
        [ProducesResponseType(typeof(UserDto), 201)]
        [ProducesResponseType(typeof(UserDto), 400)]
        public async Task<IActionResult> DeleteUser([FromBody, Required] UserDto user)
        {
            try
            {
                if (user == null)
                    return BadRequest();

                if (await _service.DeleteUser(user.Id) > 0)
                    return CreatedAtRoute("default", true);

                return BadRequest("Failed to update the database");
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }




        /// <summary>
        /// Authorizes the client app to access these APIs and return the authentication OAuth token
        /// </summary>
        /// <param name="appCredentials"></param>
        /// <returns></returns>
        [HttpPost, Route("b2b/token")]
        public async Task<IActionResult> GetToken([FromBody, Required] AppCredentialsDTO appCredentials)
        {
            try
            {
                if (appCredentials == null)
                    return BadRequest();

                var result = await _service.GetToken(appCredentials);

                return CreatedAtRoute("default", result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }


    }
}
