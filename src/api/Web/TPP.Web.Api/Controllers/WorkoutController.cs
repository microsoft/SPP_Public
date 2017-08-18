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
    [Route(ApiRoutes.WorkoutRoute)]
    public class WorkoutController : TPPBaseController
    {
        private readonly WorkoutService _service;

        public WorkoutController(TPPContext context) : base(context)
        {
            _service = new WorkoutService(context);
        }


        /// <summary>
        /// Retrieves all workouts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetWorkouts()
        {
            try
            {
                var result = await _service.GetWorkouts();

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



        /// <summary>
        /// Retrieves the session workout for the specified session Id
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        //GET api/v3/workoutDto/32
        [HttpGet("{sessionId:int}")]
        public async Task<IActionResult> GetSessionWorkout(int sessionId)
        {
            try
            {
                var result = await _service.GetSessionWorkouts(sessionId);

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





        /// <summary>
        /// Retrieves all exercises
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("exercises")]
        public async Task<IActionResult> GetAllExercises()
        {
            try
            {
                var result = await _service.GetAllExercises();

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





        #region CRUD

        // POST api/v3/workout
        /// <summary>
        /// Creates a new athlete workout
        /// </summary>
        /// <param name="workoutDto"></param>
        /// <returns>true, if successfull; otherwise - false</returns>
        /// <response code="201">true, if successfull; otherwise - false</response>
        /// <response code="400">If the object is null</response>
        [HttpPost]
        [ProducesResponseType(typeof(AthleteWorkoutDto), 201)]
        [ProducesResponseType(typeof(AthleteWorkoutDto), 400)]
        public async Task<IActionResult> CreateAthleteWorkout([FromBody, Required] AthleteWorkoutDto workoutDto)
        {
            try
            {
                if (workoutDto == null)
                    return BadRequest();

                if (await _service.CreateWorkout(workoutDto))
                    return CreatedAtRoute("default", true);

                return BadRequest("Failed to update the database");
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }






        // POST api/v3/workout/update
        /// <summary>
        /// Updates the existing workout
        /// </summary>
        /// <param name="workoutDto"></param>
        /// <returns>true, if successfull; otherwise - false</returns>
        /// <response code="201">true, if successfull; otherwise - false</response>
        /// <response code="400">If the object is null</response>
        [HttpPost, Route("update")]
        [ProducesResponseType(typeof(AthleteWorkoutDto), 201)]
        [ProducesResponseType(typeof(AthleteWorkoutDto), 400)]
        public async Task<IActionResult> UpdateAthleteWorkout([FromBody, Required] AthleteWorkoutDto workoutDto)
        {
            try
            {
                if (workoutDto == null)
                    return BadRequest();

                if (await _service.UpdateWorkout(workoutDto))
                    return CreatedAtRoute("default", true);

                return BadRequest("Failed to update the database");

            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }






        // POST api/v3/workout/exercises/update
        /// <summary>
        /// Updates the existing workout exercises
        /// </summary>
        /// <param name="workoutDto"></param>
        /// <returns>true, if successfull; otherwise - false</returns>
        /// <response code="201">true, if successfull; otherwise - false</response>
        /// <response code="400">If the object is null</response>
        [HttpPost, Route("exercises/update")]
        [ProducesResponseType(typeof(AthleteWorkoutDto), 201)]
        [ProducesResponseType(typeof(AthleteWorkoutDto), 400)]
        public async Task<IActionResult> UpdateAthleteWorkoutExercise([FromBody, Required] AthleteWorkoutDto workoutDto)
        {
            try
            {
                if (workoutDto == null)
                    return BadRequest();

                if (await _service.UpdateWorkoutExercises(workoutDto))
                    return CreatedAtRoute("default", true);

                return BadRequest("Failed to update the database");

            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }





        // POST api/v3/workout/delete
        /// <summary>
        /// Deletes the workout for the specified Id
        /// </summary>
        /// <param name="workoutId"></param>
        /// <returns>true, if successfull; otherwise - false</returns>
        /// <response code="201">true, if successfull; otherwise - false</response>
        /// <response code="400">If the object is null</response>
        [HttpPost, Route("delete/{workoutId:int}")]
        [ProducesResponseType(typeof(AthleteWorkoutDto), 201)]
        [ProducesResponseType(typeof(AthleteWorkoutDto), 400)]
        public async Task<IActionResult> DeleteAthleteWorkout(int workoutId)
        {
            try
            {
                if (workoutId <= 0)
                    return BadRequest();

                if (await _service.DeleteWorkout(workoutId))
                    return CreatedAtRoute("default", true);

                return BadRequest("Failed to update the database");
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }



        // POST api/v3/workout/deleteall
        /// <summary>
        /// Delete all workouts for the specified session Id
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns>true, if successfull; otherwise - false</returns>
        /// <response code="201">true, if successfull; otherwise - false</response>
        /// <response code="400">If the object is null</response>
        [HttpPost, Route("deleteall/{sessionId:int}")]
        [ProducesResponseType(typeof(AthleteWorkoutDto), 201)]
        [ProducesResponseType(typeof(AthleteWorkoutDto), 400)]
        public async Task<IActionResult> DeleteAllSessionWorkouts(int sessionId)
        {
            try
            {
                if (sessionId <= 0)
                    return BadRequest();

                if (await _service.DeleteAllSessionWorkouts(sessionId))
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
