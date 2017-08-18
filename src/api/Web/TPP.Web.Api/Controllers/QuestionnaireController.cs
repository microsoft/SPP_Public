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
    [Route(ApiRoutes.QuestionnaireRoute)]
    public class QuestionnaireController : TPPBaseController
    {
        private readonly QuestionnaireService _service;

        public QuestionnaireController(TPPContext context) : base(context)
        {
            _service = new QuestionnaireService(context);
        }


        /// <summary>
        /// Get the questioonnaire for the current session
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        //GET api/v3/questionnaires/5
        [HttpGet("{sessionId:int}")]
        public async Task<IActionResult> GetQuestionnaire(int sessionId)
        {
            try
            {
                var result = await _service.GetAthleteQuestions(sessionId);

                if (result == null)
                    return NotFound();
                if (!result.Questions.Any())
                    return NoContent();

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                if (ex.Message.StartsWith("Sequence contains no matching element"))
                    return NotFound();
                return BadRequest(ex.Message);
            }
        }



        /// <summary>
        /// Get the list of questionnaires for the current session
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        //GET api/v3/questionnaires/list/5
        [HttpGet, Route("list/{sessionId:int}")]
        public async Task<IActionResult> GetSessionQuestionnaires(int sessionId)
        {
            try
            {
                var result = await _service.GetSessionQuestionnaires(sessionId);

                if (result == null)
                    return NotFound();
                if (!result.Any())
                    return NoContent();

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                if (ex.Message.StartsWith("Sequence contains no matching element"))
                    return NotFound();
                return BadRequest(ex.Message);
            }
        }



        /// <summary>
        /// Retrieves questionnaire entity by its Id
        /// </summary>
        /// <param name="questionnaireId"></param>
        /// <returns></returns>
        //GET api/v3/questionnaires/entity/5
        [HttpGet, Route("entity/{questionnaireId:int}")]
        public async Task<IActionResult> Get(int questionnaireId)
        {
            try
            {
                var result = await _service.GetQuestionnaire(questionnaireId);

                if (result == null)
                    return NotFound();

                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }



        /// <summary>
        /// Retrieves question's response for the given session and player IDs
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        //GET api/v3/questionnaires/response/5/1
        [HttpGet, Route("response/{sessionId:int}/{playerId:int}")]
        public async Task<IActionResult> GetQuestionResponse(int sessionId, int playerId)
        {
            try
            {
                var result = await _service.GetPlayerResponse(sessionId, playerId);

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



        // POST api/v3/questionnaires
        /// <summary>
        /// Creates a new questionnaire
        /// </summary>
        /// <param name="questionnaireDto"></param>
        /// <returns>Newly created questionnaireDto</returns>
        /// <response code="201">Returns the number of created records in the database</response>
        /// <response code="400">If the object is null</response>
        [HttpPost]
        [ProducesResponseType(typeof(AthleteQuestionnaireDto), 201)]
        [ProducesResponseType(typeof(AthleteQuestionnaireDto), 400)]
        public async Task<IActionResult> CreateQuestionnaire([FromBody, Required] AthleteQuestionnaireDto questionnaireDto)
        {
            try
            {
                if (questionnaireDto == null)
                    return BadRequest();

                var result = await _service.CreateQuestionnaire(questionnaireDto);

                return CreatedAtRoute("default", result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }




        // POST api/v3/questionnaires/update
        /// <summary>
        /// Updates the existing questionnaire
        /// </summary>
        /// <param name="questionnaireDto"></param>
        /// <returns>Newly created questionnaireDto</returns>
        /// <response code="201">Returns the number of updated records</response>
        /// <response code="400">If the object is null</response>
        [HttpPost, Route("update")]
        [ProducesResponseType(typeof(AthleteQuestionnaireDto), 201)]
        [ProducesResponseType(typeof(AthleteQuestionnaireDto), 400)]
        public async Task<IActionResult> UpdateQuestionnaire([FromBody, Required] AthleteQuestionnaireDto questionnaireDto)
        {
            try
            {
                if (questionnaireDto == null)
                    return BadRequest();

                var updatedRows = await _service.UpdateQuestionnaire(questionnaireDto);

                return CreatedAtRoute("default", updatedRows);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }



        // POST api/v3/questionnaires/delete/5
        /// <summary>
        /// Deletes a questionnaire with the specified Id
        /// </summary>
        /// <param name="questionnaireId"></param>
        /// <returns>Newly created questionnaireDto</returns>
        /// <response code="201">Returns the number of updated records</response>
        /// <response code="400">If the object is null</response>
        [HttpPost, Route("delete/{questionnaireId:int}")]
        [ProducesResponseType(typeof(AthleteQuestionnaireDto), 201)]
        [ProducesResponseType(typeof(AthleteQuestionnaireDto), 400)]
        public async Task<IActionResult> RemoveQuestionnaire(int questionnaireId)
        {
            try
            {
                if (questionnaireId <= 0)
                    return BadRequest();

                var updatedRows = await _service.DeleteQuestionnaire(questionnaireId);

                return CreatedAtRoute("default", updatedRows);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }


    }
}
