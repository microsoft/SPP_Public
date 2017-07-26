// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.Collections.Generic;
using System.Threading.Tasks;
using TPP.Core.Services.Models;

namespace TPP.Core.Services.Contracts
{
    public interface IDrillService : IEntityConfiguration
    {
        //CREATE APIs

        //RETRIEVE APIs
        Task<IList<DrillDto>> GetAllDrills();

        //UPDATE APIs


        //DELETE APIs

    }
}