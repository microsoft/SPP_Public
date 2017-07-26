// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Events;
using TPP.Core.Data;
using TPP.Core.Services.Impl;
using TPP.Core.Services.Models;

namespace TPP.Api.Controllers.V2
{
    [Authorize]
    [Route(ApiRoutes.PlayerResponseRoute)]
    public class PlayerResponseController : TPPBaseController
    {
        private readonly PlayerResponseService _service;

        public PlayerResponseController(TPPContext context) : base(context)
        {
            _service = new PlayerResponseService(context);
        }


        // POST api/v3/response
        /// <summary>
        /// Stores the player's response to the Questionnaire into the Database
        /// </summary>
        /// <param name="playerResponseDto"></param>
        /// <returns>Newly created response</returns>
        /// <response code="201">Returns the newly created PlayerResponseDto object</response>
        /// <response code="400">If the object is null</response>
        [HttpPost]
        [ProducesResponseType(typeof(PlayerResponseDto), 201)]
        [ProducesResponseType(typeof(PlayerResponseDto), 400)]
        public async Task<IActionResult> SaveResponse([FromBody, Required] PlayerResponseDto playerResponseDto)
        {
            try
            {
                if (playerResponseDto == null)
                    return BadRequest();

                await _service.CreatePlayerResponse(playerResponseDto);

                return CreatedAtRoute("default", playerResponseDto);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }



 
#region CRUD

        // POST api/v3/response/create
        /// <summary>
        /// Adds a new player's response
        /// </summary>
        /// <param name="playerResponseDto"></param>
        /// <returns>true, if successfull; otherwise - false</returns>
        /// <response code="201">true, if successfull; otherwise - false</response>
        /// <response code="400">If the object is null</response>
        [HttpPost, Route("create")]
        [ProducesResponseType(typeof(PlayerResponseDto), 201)]
        [ProducesResponseType(typeof(PlayerResponseDto), 400)]
        public async Task<IActionResult> CreatePlayerResponse([FromBody, Required] PlayerResponseDto playerResponseDto)
        {
            try
            {
                if (playerResponseDto == null)
                    return BadRequest();

                if (await _service.CreatePlayerResponse(playerResponseDto))
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
        /// Retrieves the player's responses by the Player Id and Questionnaire Id
        /// </summary>
        /// <param name="playerResponseDto"></param>
        /// <returns></returns>
        //GET api/v3/response/
        [HttpGet]
        public async Task<IActionResult> Get([FromBody, Required] PlayerResponseDto playerResponseDto)
        {
            try
            {
                var result = await _service.GetPlayerResponse(playerResponseDto);

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves the player's responses by the Player Id and Questionnaire Id
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="questionnaireId"></param>
        /// <returns></returns>
        //GET api/v3/response/1/2
        [HttpGet, Route("{playerId:int}/{questionnaireId:int}")]
        public async Task<IActionResult> Get(int playerId, int questionnaireId)
        {
            try
            {
                bool result = await _service.GetQuestionnairePlayerResponse(playerId, questionnaireId);

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }


        // POST api/v3/response/update
        /// <summary>
        /// Updates the existing response
        /// </summary>
        /// <param name="playerResponseDto"></param>
        /// <returns>true, if successfull; otherwise - false</returns>
        /// <response code="201">true, if successfull; otherwise - false</response>
        /// <response code="400">If the object is null</response>
        [HttpPost, Route("update")]
        [ProducesResponseType(typeof(PlayerResponseDto), 201)]
        [ProducesResponseType(typeof(PlayerResponseDto), 400)]
        public async Task<IActionResult> UpdatePlayerResponse([FromBody, Required] PlayerResponseDto playerResponseDto)
        {
            try
            {
                if (playerResponseDto == null)
                    return BadRequest();

                if (await _service.UpdatePlayerResponse (playerResponseDto))
                    return CreatedAtRoute("default", true);

                return BadRequest("Failed to update the database");

            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }




        // POST api/v3/response/delete
        /// <summary>
        /// Deletes the response with the specified Id
        /// </summary>
        /// <param name="playerResponseId"></param>
        /// <returns>true, if successfull; otherwise - false</returns>
        /// <response code="201">true, if successfull; otherwise - false</response>
        /// <response code="400">If the object is null</response>
        [HttpPost, Route("delete/{playerResponseId:int}")]
        [ProducesResponseType(typeof(PlayerResponseDto), 201)]
        [ProducesResponseType(typeof(PlayerResponseDto), 400)]
        public async Task<IActionResult> DeletePlayerResponse(int playerResponseId)
        {
            try
            {
                if (playerResponseId <= 0)
                    return BadRequest();

                if (await _service.DeletePlayerResponse (playerResponseId))
                    return CreatedAtRoute("default", true);

                return BadRequest("Failed to update the database");
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
#endregion



    }
}
