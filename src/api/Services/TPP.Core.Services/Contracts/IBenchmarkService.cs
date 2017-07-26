// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.Collections.Generic;
using System.Threading.Tasks;
using TPP.Core.Data.Entities;
using TPP.Core.Services.Models;

namespace TPP.Core.Services.Contracts
{
    public interface IBenchmarkService : IEntityConfiguration
    {
        //CREATE APIs
        Task<int> AddBenchmark(Benchmark data);

        //RETRIEVE APIs

        //UPDATE APIs


        //DELETE APIs

    }
}