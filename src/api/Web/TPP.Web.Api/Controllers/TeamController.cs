// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using TPP.Core.Services.Models;

namespace TPP.Api.Controllers.V1
{
    using System;
    using System.Threading.Tasks;

    using Core.Data;
    using Core.Services.Impl;

    using Microsoft.AspNetCore.Mvc;

    using Serilog;
    using Serilog.Events;

    [Authorize]
    [Route(ApiRoutes.TeamRoute)]
    public class TeamController : TPPBaseController
    {
        private readonly TeamService _service;

        public TeamController(TPPContext context) : base(context)
        {
            this._service = new TeamService(context);
        }


        /// <summary>
        /// Returns all teams
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public async Task<IActionResult> GetAllTeams()
        {
            try
            {
                var result = await this._service.GetTeams();

                if (result == null || !result.Any())
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return this.BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Returns the team for the specified Team Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await this._service.GetTeam(id);

                if (result == null)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return this.BadRequest(ex.Message);
            }
        }




        /// <summary>
        /// Returns the team by the specified Team Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("get/{name}")]
        public async Task<IActionResult> GetTeamByName(string name)
        {
            try
            {
                var result = await this._service.GetTeamByName(name);

                if (result == null)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return this.BadRequest(ex.Message);
            }
        }



        /// <summary>
        /// Returns the domestic players for the specified Team Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("players/{id:int}")]
        public async Task<IActionResult> GetDomesticPlayers(int id)
        {
            try
            {
                var result = await this._service.GetDomesticTeamPlayers(id);

                if (result == null)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return this.BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Retrieves Team Readiness data for the last 6 months and the current month for the given Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}/readiness")]
        public async Task<IActionResult> GetTeamReadiness(int id)
        {
            try
            {
                var result = await this._service.GetTeamReadiness(id);

                if (result == null)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return this.BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Retrieves information about the next match for the specified Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}/nextmatch")]
        public async Task<IActionResult> GetNextTeamMatchInfo(int id)
        {
            try
            {
                var result = await this._service.GetNextMatchInfo(id, DateTime.UtcNow);

                if (result == null)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return this.BadRequest(ex.Message);
            }
        }




        /// <summary>
        /// Retrieves information about the all team matches for the specified team
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}/allmatches")]
        public async Task<IActionResult> GetMatchHistoryInfo(int id)
        {
            try
            {
                var result = await this._service.GetMatchHistoryInfo(id);

                if (result == null)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return this.BadRequest(ex.Message);
            }
        }





        /// <summary>
        /// Retrieves the information about readiness stats of all player for the specified Team Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}/teamtrending")]
        public async Task<IActionResult> GetPlayerReadiness(int id)
        {
            try
            {
                var result = await this._service.GetPlayersReadiness(id);

                if (result == null)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return this.BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Retrieves Team Images used in the gallery carousel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}/images")]
        public async Task<IActionResult> GetTeamImages(int id)
        {
            try
            {
                var result = await this._service.GetTeamImages(id);

                if (result == null)
                {
                    return this.NotFound();
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return this.BadRequest(ex.Message);
            }
        }


        // POST api/v3/teams/images
        /// <summary>
        /// Adds the list of images to the team
        /// </summary>
        /// <param name="teamImages"></param>
        /// <returns>true, if successfull; otherwise - false</returns>
        /// <response code="201">true, if successfull; otherwise - false</response>
        /// <response code="400">If the object is null</response>
        [HttpPost, Route("images/add")]
        [ProducesResponseType(typeof(TeamImagesDto), 201)]
        [ProducesResponseType(typeof(TeamImagesDto), 400)]
        public async Task<IActionResult> AddTeamImages([FromBody, Required] TeamImagesDto teamImages)
        {
            try
            {
                if (teamImages == null)
                    return BadRequest();

                if (await _service.AddTeamImages(teamImages))
                    return CreatedAtRoute("default", true);

                return BadRequest("Failed to update the database");
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }





        // POST api/v3/teams
        /// <summary>
        /// Creates a new team
        /// </summary>
        /// <param name="teamDto"></param>
        /// <returns>true, if successfull; otherwise - false</returns>
        /// <response code="201">true, if successfull; otherwise - false</response>
        /// <response code="400">If the object is null</response>
        [HttpPost]
        [ProducesResponseType(typeof(TeamDto), 201)]
        [ProducesResponseType(typeof(TeamDto), 400)]
        public async Task<IActionResult> CreateTeam([FromBody, Required] TeamDto teamDto)
        {
            try
            {
                if (teamDto == null)
                    return BadRequest();

                var result = await _service.CreateTeam(teamDto);
                return CreatedAtRoute("default", result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }




        // POST api/v3/teams/update
        /// <summary>
        /// Updates the existing team
        /// </summary>
        /// <param name="teamDto"></param>
        /// <returns>true, if successfull; otherwise - false</returns>
        /// <response code="201">true, if successfull; otherwise - false</response>
        /// <response code="400">If the object is null</response>
        [HttpPost, Route("update")]
        [ProducesResponseType(typeof(TeamDto), 201)]
        [ProducesResponseType(typeof(TeamDto), 400)]
        public async Task<IActionResult> UpdateTeam([FromBody, Required] TeamDto teamDto)
        {
            try
            {
                if (teamDto == null)
                    return BadRequest();

                if (await _service.UpdateTeam (teamDto))
                    return CreatedAtRoute("default", true);

                return BadRequest("Failed to update the database");

            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }




        // POST api/v3/teams/delete
        /// <summary>
        /// Deletes the team using the specified Team Id
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns>true, if successfull; otherwise - false</returns>
        /// <response code="201">true, if successfull; otherwise - false</response>
        /// <response code="400">If the object is null</response>
        [HttpPost, Route("delete/{teamId:int}")]
        [ProducesResponseType(typeof(TeamDto), 201)]
        [ProducesResponseType(typeof(TeamDto), 400)]
        public async Task<IActionResult> DeleteTeam(int teamId)
        {
            try
            {
                if (teamId <= 0)
                    return BadRequest();

                if (await _service.DeleteTeam (teamId))
                    return CreatedAtRoute("default", true);

                return BadRequest("Failed to update the database");
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

    }
}
