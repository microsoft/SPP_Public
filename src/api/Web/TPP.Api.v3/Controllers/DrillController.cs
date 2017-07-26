// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
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

namespace TPP.Api.Controllers.V3
{
    [Authorize]
    [Route(ApiRoutes.DrillRoute)]
    public class DrillController : TPPBaseController
    {
        private readonly DrillService _service;

        public DrillController(TPPContext context) : base(context)
        {
            _service = new DrillService(context);
        }


        /// <summary>
        /// Retrieves all drills
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllDrills()
        {
            try
            {
                var drills = await _service.GetAllDrills();

                if (drills == null)
                    return NotFound();

                return new ObjectResult(drills);

            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }





    }
}
