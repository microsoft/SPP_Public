// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    [Route(ApiRoutes.SettingsRoute)]
    public class SettingsController: TPPBaseController
    {
        private readonly SettingsService _service;
        private readonly SessionService _sessionservice;

        public SettingsController(TPPContext context) : base(context)
        {
            this._service = new SettingsService(context);
            this._sessionservice = new SessionService(context);
        }

        /// <summary>
        /// Returns the player positions.
        /// </summary>
        /// <returns></returns>s
        [HttpGet("positions")]
        public async Task<IActionResult> GetPlayerPositions()
        {
            try
            {
                var result = await this._service.GetPlayerPositions();

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return this.BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Creates a new player position
        /// </summary>
        /// <param name="playerPositionDto"></param>
        /// <returns>true, if successfull; otherwise - false</returns>
        /// <response code="201">true, if successfull; otherwise - false</response>
        /// <response code="400">If the object is null</response>
        [HttpPost, Route("create/position")]
        [ProducesResponseType(typeof(PlayerPositionDto), 201)]
        [ProducesResponseType(typeof(PlayerPositionDto), 400)]
        public async Task<IActionResult> CreatePlayerPosition([FromBody, Required] PlayerPositionDto playerPositionDto)
        {
            try
            {
                if (playerPositionDto == null)
                    return BadRequest();

                var result = await _service.CreatePlayerPosition(playerPositionDto);
                return CreatedAtRoute("default", result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates the existing player position
        /// </summary>
        /// <param name="playerPositionDto"></param>
        /// <returns>true, if successfull; otherwise - false</returns>
        /// <response code="201">true, if successfull; otherwise - false</response>
        /// <response code="400">If the object is null</response>
        [HttpPost, Route("update/position")]
        [ProducesResponseType(typeof(PlayerPositionDto), 201)]
        [ProducesResponseType(typeof(PlayerPositionDto), 400)]
        public async Task<IActionResult> UpdatePlayerPosition([FromBody, Required] PlayerPositionDto playerPositionDto)
        {
            try
            {
                if (playerPositionDto == null)
                    return BadRequest();

                if (await _service.UpdatePlayerPosition(playerPositionDto))
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
        /// Deletes the player position using the specified  Id
        /// </summary>
        /// <param name="positionId"></param>
        /// <returns>true, if successfull; otherwise - false</returns>
        /// <response code="201">true, if successfull; otherwise - false</response>
        /// <response code="400">If the object is null</response>
        [HttpPost, Route("delete/position/{positionId:int}")]
        [ProducesResponseType(typeof(PlayerPositionDto), 201)]
        [ProducesResponseType(typeof(PlayerPositionDto), 400)]
        public async Task<IActionResult> DeletePlayerPosition(int positionId)
        {
            try
            {
                if (positionId <= 0)
                    return BadRequest();

                if (await _service.DeletePlayerPosition(positionId))
                    return CreatedAtRoute("default", true);

                return BadRequest("Failed to update the database");
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // SUB POSITIONS 

        /// <summary>
        /// Returns the sub positions.
        /// </summary>
        /// <returns></returns>s
        [HttpGet("subpositions")]
        public async Task<IActionResult> GetSubPositions()
        {
            try
            {
                var result = await this._service.GetSubPositions();

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return this.BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Creates a new sub position
        /// </summary>
        /// <param name="subPositionDto"></param>
        /// <returns>true, if successfull; otherwise - false</returns>
        /// <response code="201">true, if successfull; otherwise - false</response>
        /// <response code="400">If the object is null</response>
        [HttpPost, Route("create/subposition")]
        [ProducesResponseType(typeof(SubPositionDto), 201)]
        [ProducesResponseType(typeof(SubPositionDto), 400)]
        public async Task<IActionResult> CreateSubPosition([FromBody, Required] SubPositionDto subPositionDto)
        {
            try
            {
                if (subPositionDto == null)
                    return BadRequest();

                var result = await _service.CreateSubPosition(subPositionDto);
                return CreatedAtRoute("default", result);

            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates the existing sub position
        /// </summary>
        /// <param name="subPositionDto"></param>
        /// <returns>true, if successfull; otherwise - false</returns>
        /// <response code="201">true, if successfull; otherwise - false</response>
        /// <response code="400">If the object is null</response>
        [HttpPost, Route("update/subposition")]
        [ProducesResponseType(typeof(SubPositionDto), 201)]
        [ProducesResponseType(typeof(SubPositionDto), 400)]
        public async Task<IActionResult> UpdateSubPosition([FromBody, Required] SubPositionDto subPositionDto)
        {
            try
            {
                if (subPositionDto == null)
                    return BadRequest();

                if (await _service.UpdateSubPosition(subPositionDto))
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
        /// Deletes the sub position using the specified  Id
        /// </summary>
        /// <param name="subpositionId"></param>
        /// <returns>true, if successfull; otherwise - false</returns>
        /// <response code="201">true, if successfull; otherwise - false</response>
        /// <response code="400">If the object is null</response>
        [HttpPost, Route("delete/subposition/{subpositionId:int}")]
        [ProducesResponseType(typeof(SubPositionDto), 201)]
        [ProducesResponseType(typeof(SubPositionDto), 400)]
        public async Task<IActionResult> DeleteSubPosition(int subpositionId)
        {
            try
            {
                if (subpositionId <= 0)
                    return BadRequest();

                if (await _service.DeleteSubPosition(subpositionId))
                    return CreatedAtRoute("default", true);

                return BadRequest("Failed to update the database");
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }




        //// MESOCYCLES 

        /// <summary>
        /// Returns the Mesocycles.
        /// </summary>
        /// <returns></returns>s
        [HttpGet("mesocycles")]
        public async Task<IActionResult> GetMesocycles()
        {
            try
            {
                var result = await this._service.GetMesocycles();

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return this.BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Creates a new mesocycleDto
        /// </summary>
        /// <param name="mesocycleDto"></param>
        /// <returns>true, if successfull; otherwise - false</returns>
        /// <response code="201">true, if successfull; otherwise - false</response>
        /// <response code="400">If the object is null</response>
        [HttpPost, Route("create/mesocycle")]
        [ProducesResponseType(typeof(MesocycleDto), 201)]
        [ProducesResponseType(typeof(MesocycleDto), 400)]
        public async Task<IActionResult> CreateMesocycle([FromBody, Required] MesocycleDto mesocycleDto)
        {
            try
            {
                if (mesocycleDto == null)
                    return BadRequest();

                var result = await _service.CreateMesocycle(mesocycleDto);

                return CreatedAtRoute("default", result);

            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates the existing mesocycle
        /// </summary>
        /// <param name="mesocycleDto"></param>
        /// <returns>true, if successfull; otherwise - false</returns>
        /// <response code="201">true, if successfull; otherwise - false</response>
        /// <response code="400">If the object is null</response>
        [HttpPost, Route("update/mesocycle")]
        [ProducesResponseType(typeof(MesocycleDto), 201)]
        [ProducesResponseType(typeof(MesocycleDto), 400)]
        public async Task<IActionResult> UpdateMesocycle([FromBody, Required] MesocycleDto mesocycleDto)
        {
            try
            {
                if (mesocycleDto == null)
                    return BadRequest();

                if (await _service.UpdateMesocycle(mesocycleDto))
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
        /// Deletes the mesocycle using the specified  Id
        /// </summary>
        /// <param name="mesocycleId"></param>
        /// <returns>true, if successfull; otherwise - false</returns>
        /// <response code="201">true, if successfull; otherwise - false</response>
        /// <response code="400">If the object is null</response>
        [HttpPost, Route("delete/mesocycle/{mesocycleid:int}")]
        [ProducesResponseType(typeof(MesocycleDto), 201)]
        [ProducesResponseType(typeof(MesocycleDto), 400)]
        public async Task<IActionResult> DeleteMesocycle(int mesocycleId)
        {
            try
            {
                if (mesocycleId <= 0)
                    return BadRequest();

                if (await _service.DeleteMesocycle(mesocycleId))
                    return CreatedAtRoute("default", true);

                return BadRequest("Failed to update the database");
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        //// IMAGES 

        /// <summary>
        /// Returns the Images.
        /// </summary>
        /// <returns></returns>s
        [HttpGet("images")]
        public async Task<IActionResult> GetImages()
        {
            try
            {
                var result = await this._service.GetImages();

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return this.BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Creates a new image
        /// </summary>
        /// <param name="imageDto"></param>
        /// <returns>true, if successfull; otherwise - false</returns>
        /// <response code="201">true, if successfull; otherwise - false</response>
        /// <response code="400">If the object is null</response>
        [HttpPost, Route("create/image")]
        [ProducesResponseType(typeof(ImageDto), 201)]
        [ProducesResponseType(typeof(ImageDto), 400)]
        public async Task<IActionResult> CreateImage([FromBody, Required] ImageDto imageDto)
        {
            try
            {
                if (imageDto == null)
                    return BadRequest();

                var result = await _service.CreateImage(imageDto);

                return CreatedAtRoute("default", result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates the existing image
        /// </summary>
        /// <param name="imageDto"></param>
        /// <returns>true, if successfull; otherwise - false</returns>
        /// <response code="201">true, if successfull; otherwise - false</response>
        /// <response code="400">If the object is null</response>
        [HttpPost, Route("update/image")]
        [ProducesResponseType(typeof(ImageDto), 201)]
        [ProducesResponseType(typeof(ImageDto), 400)]
        public async Task<IActionResult> UpdateImage([FromBody, Required] ImageDto imageDto)
        {
            try
            {
                if (imageDto == null)
                    return BadRequest();

                if (await _service.UpdateImage(imageDto))
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
        /// Deletes the image using the specified  Id
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns>true, if successfull; otherwise - false</returns>
        /// <response code="201">true, if successfull; otherwise - false</response>
        /// <response code="400">If the object is null</response>
        [HttpPost, Route("delete/image/{imageId:int}")]
        [ProducesResponseType(typeof(ImageDto), 201)]
        [ProducesResponseType(typeof(ImageDto), 400)]
        public async Task<IActionResult> DeleteImage(int imageId)
        {
            try
            {
                if (imageId <= 0)
                    return BadRequest();

                if (await _service.DeleteImage(imageId))
                    return CreatedAtRoute("default", true);

                return BadRequest("Failed to update the database");
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }




        // MICROCYCLE 

        /// <summary>
        /// Returns the microcycles.
        /// </summary>
        /// <returns></returns>s
        [HttpGet("microcycles")]
        public async Task<IActionResult> GetMicrocycles()
        {
            try
            {
                var result = await this._service.GetMicrocycles();

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return this.BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Creates a new microcycle
        /// </summary>
        /// <param name="microcycleDTO"></param>
        /// <returns>true, if successfull; otherwise - false</returns>
        /// <response code="201">true, if successfull; otherwise - false</response>
        /// <response code="400">If the object is null</response>
        [HttpPost, Route("create/microcycle")]
        [ProducesResponseType(typeof(MicrocycleDto), 201)]
        [ProducesResponseType(typeof(MicrocycleDto), 400)]
        public async Task<IActionResult> CreateMicrocycle([FromBody, Required] MicrocycleDto microcycleDTO)
        {
            try
            {
                if (microcycleDTO == null)
                    return BadRequest();

                var result = await _service.CreateMicrocycle(microcycleDTO);
                return CreatedAtRoute("default", result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates the existing Microcycle
        /// </summary>
        /// <param name="microcycleDto"></param>
        /// <returns>true, if successfull; otherwise - false</returns>
        /// <response code="201">true, if successfull; otherwise - false</response>
        /// <response code="400">If the object is null</response>
        [HttpPost, Route("update/microcycle")]
        [ProducesResponseType(typeof(MicrocycleDto), 201)]
        [ProducesResponseType(typeof(MicrocycleDto), 400)]
        public async Task<IActionResult> UpdateSubPosition([FromBody, Required] MicrocycleDto microcycleDto)
        {
            try
            {
                if (microcycleDto == null)
                    return BadRequest();

                if (await _service.UpdateMicrocycle(microcycleDto))
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
        /// Deletes the microcycle using the specified  Id
        /// </summary>
        /// <param name="microsycleId"></param>
        /// <returns>true, if successfull; otherwise - false</returns>
        /// <response code="201">true, if successfull; otherwise - false</response>
        /// <response code="400">If the object is null</response>
        [HttpPost, Route("delete/microcycle/{microsycleid:int}")]
        [ProducesResponseType(typeof(MicrocycleDto), 201)]
        [ProducesResponseType(typeof(MicrocycleDto), 400)]
        public async Task<IActionResult> DeleteMicrocycle(int microsycleId)
        {
            try
            {
                if (microsycleId <= 0)
                    return BadRequest();

                if (await _service.DeleteMicrocycle(microsycleId))
                    return CreatedAtRoute("default", true);

                return BadRequest("Failed to update the database");
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }



        // SESSION USER 
        
        /// <summary>
        /// Creates a new SessionUser
        /// </summary>
        /// <param name="sessionUserDto"></param>
        /// <returns>true, if successfull; otherwise - false</returns>
        /// <response code="201">true, if successfull; otherwise - false</response>
        /// <response code="400">If the object is null</response>
        [HttpPost, Route("add/sessionuser")]
        [ProducesResponseType(typeof(SessionUserDto), 201)]
        [ProducesResponseType(typeof(SessionUserDto), 400)]
        public async Task<IActionResult> AddSessionUser([FromBody, Required] SessionUserDto sessionUserDto)
        {
            try
            {
                if (sessionUserDto == null)
                    return BadRequest();

                if (await _sessionservice.CreateSessionUser(sessionUserDto))
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
