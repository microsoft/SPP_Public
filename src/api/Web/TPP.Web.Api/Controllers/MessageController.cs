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

namespace TPP.Api.Controllers.V1
{
    [Authorize]
    [Route(ApiRoutes.MessageRoute)]
    public class MessageController : TPPBaseController
    {
        private readonly MessageService _service;

        public MessageController(TPPContext context) : base(context)
        {
            _service = new MessageService(context);
        }


        /// <summary>
        /// Retrieves the message content by its Id
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        //GET api/v2/messages/id/1
        [HttpGet("id/{messageId:int}")]
        public async Task<IActionResult> Get(int messageId)
        {
            try
            {
                var result = await _service.GetMessage(messageId);

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Retrieves all messages for the current player
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        //GET api/v2/messages/5
        [HttpGet("{playerId:int}")]
        public async Task<IActionResult> GetMessages(int playerId)
        {
            try
            {
                var result = await _service.GetMessages(playerId);

                if (result == null)
                    return NotFound();
                if (!result.Any())
                    return NoContent();

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }




        // POST api/v2/messages
        /// <summary>
        /// Adds a new message to the database
        /// </summary>
        /// <param name="messageDto"></param>
        /// <returns>true, if successfull; otherwise - false</returns>
        /// <response code="201">true, if successfull; otherwise - false</response>
        /// <response code="400">If the object is null</response>
        [HttpPost]
        [ProducesResponseType(typeof(MessageDto), 201)]
        [ProducesResponseType(typeof(MessageDto), 400)]
        public async Task<IActionResult> AddMessage([FromBody, Required] MessageDto messageDto)
        {
            try
            {
                if (messageDto == null)
                    return BadRequest();

                var result = await _service.CreateMessage(messageDto);
                return CreatedAtRoute("default", result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }




        // POST api/v2/messages/update
        /// <summary>
        /// Updates the existing message
        /// </summary>
        /// <param name="messageDto"></param>
        /// <returns>true, if successfull; otherwise - false</returns>
        /// <response code="201">true, if successfull; otherwise - false</response>
        /// <response code="400">If the object is null</response>
        [HttpPost, Route("update")]
        [ProducesResponseType(typeof(MessageDto), 201)]
        [ProducesResponseType(typeof(MessageDto), 400)]
        public async Task<IActionResult> UpdateMessage([FromBody, Required] MessageDto messageDto)
        {
            try
            {
                if (messageDto == null)
                    return BadRequest();

                if(await _service.UpdateMessage(messageDto))
                    return CreatedAtRoute("default", true);

                return BadRequest("Failed to update the database");

            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }




        // POST api/v2/messages/delete
        /// <summary>
        /// Deletes the message with the specified Id
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns>true, if successfull; otherwise - false</returns>
        /// <response code="201">true, if successfull; otherwise - false</response>
        /// <response code="400">If the object is null</response>
        [HttpPost, Route("delete/{messageId:int}")]
        [ProducesResponseType(typeof(MessageDto), 201)]
        [ProducesResponseType(typeof(MessageDto), 400)]
        public async Task<IActionResult> DeleteMessage(int messageId)
        {
            try
            {
                if (messageId <= 0)
                    return BadRequest();

                if (await _service.DeleteMessage(messageId))
                    return CreatedAtRoute("default", true);

                return BadRequest("Failed to update the database");
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }



        // POST api/v2/messages/deleteall
        /// <summary>
        /// Deletes all messages for the current player
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns>true, if successfull; otherwise - false</returns>
        /// <response code="201">true, if successfull; otherwise - false</response>
        /// <response code="400">If the object is null</response>
        [HttpPost, Route("deleteall/{playerId:int}")]
        [ProducesResponseType(typeof(MessageDto), 201)]
        [ProducesResponseType(typeof(MessageDto), 400)]
        public async Task<IActionResult> DeletePlayerMessages(int playerId)
        {
            try
            {
                if (playerId <= 0)
                    return BadRequest();

                if (await _service.DeletePlayerMessages(playerId))
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
