using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using TPP.Core.Data;
using TPP.Core.Services.Impl;
using TPP.Core.Services.Models;

namespace TPP.Api.Controllers.V1
{
    [Authorize]
    [Route(ApiRoutes.LocationRoute)]
    public class LocationController : TPPBaseController
    {
        private readonly LocationService _service;

        public LocationController(TPPContext context) : base(context)
        {
            _service = new LocationService(context);
        }



        /// <summary>
        /// Retrieves all locations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _service.GetLocations();

                if (result == null)
                    return NotFound();
                if(!result.Any())
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





        // POST api/v1/locations
        /// <summary>
        /// Creates a new location
        /// </summary>
        /// <param name="locationDto"></param>
        /// <returns>true, if successfull; otherwise - false</returns>
        /// <response code="201">true, if successfull; otherwise - false</response>
        /// <response code="400">If the object is null</response>
        [HttpPost]
        [ProducesResponseType(typeof(LocationDto), 201)]
        [ProducesResponseType(typeof(LocationDto), 400)]
        public async Task<IActionResult> CreateLocation([FromBody, Required] LocationDto locationDto)
        {
            try
            {
                if (locationDto == null)
                    return BadRequest();

                var result = await _service.CreateLocation(locationDto);
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
