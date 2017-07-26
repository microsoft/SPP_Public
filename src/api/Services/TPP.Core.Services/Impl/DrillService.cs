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

    public class DrillProfile : Profile
    {
        public DrillProfile()
        {
            CreateMap<Drill, DrillDto>();
            CreateMap<DrillDto, Drill>();
            CreateMap<Image, ImageDto>();
        }
    }

    public class DrillService : TPPDbService, IDrillService
    {
        private IDbConnection _db;

        public DrillService(TPPContext context) : base(context)
        {
            _db = context?.TppDbConnection;
            RegisterEntities();
        }

        public DrillService(BaseDbContext context) : base(context)
        {
            _db = context.DbConnection;
            RegisterEntities();
        }


        public void RegisterEntities()
        {
            OrmConfiguration.DefaultDialect = SqlDialect.MsSql;

            //Initialize static mapping
            AutoMapperConfiguration.Configure();
        }





        public async Task<IList<DrillDto>> GetAllDrills()
        {
            var drillsDb = await _db.FindAsync<Drill>(query => query
                .Include<Image>(join => join.InnerJoin()));

            return drillsDb.Select(rec =>
                Mapper.Map<DrillDto>(rec)).ToList();

        }




    }

}