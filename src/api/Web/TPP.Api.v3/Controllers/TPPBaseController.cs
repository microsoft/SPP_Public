// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Mvc;
using TPP.Core.Data;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TPP.Api.Controllers
{
    public class TPPBaseController : Controller
    {
        private readonly TPPContext _context;
        private readonly BaseDbContext _testContext;

        public TPPBaseController(TPPContext context)
        {
            _context = context;
        }


        //Unit Tests
        public TPPBaseController(BaseDbContext context)
        {
            _testContext = context;
        }

    }
}
