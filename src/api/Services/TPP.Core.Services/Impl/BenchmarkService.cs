// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dapper.FastCrud;
using TPP.Core.Data;
using TPP.Core.Data.Entities;
using TPP.Core.Services.Contracts;
using TPP.Core.Services.Models;

namespace TPP.Core.Services.Impl
{


    public class BenchmarkService : TPPDbService, IBenchmarkService
    {
        private IDbConnection _db;

        public BenchmarkService(TPPContext context) : base(context)
        {
            _db = context?.TppDbConnection;
        }

        public BenchmarkService(BaseDbContext context) : base(context)
        {
            _db = context.DbConnection;
        }



        public void RegisterEntities()
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> AddBenchmark(Benchmark data)
        {
            //Insert into Benchmark table
            await _db.InsertAsync(data);

            return data.Id;
        }
    }

}