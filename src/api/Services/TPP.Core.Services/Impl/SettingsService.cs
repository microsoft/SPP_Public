// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using System.Collections;
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
    public class SettingsProfile : Profile {
        public SettingsProfile()
        {
            CreateMap<Position, PlayerPositionDto>();
            CreateMap<PlayerPositionDto, Position>();
            CreateMap<SubPosition, SubPositionDto>();
            CreateMap<SubPositionDto, SubPosition>();
            CreateMap<Microcycle, MicrocycleDto>();
            CreateMap<MicrocycleDto, Microcycle>();
            CreateMap<Mesocycle, MesocycleDto>();
            CreateMap<MesocycleDto, Mesocycle>();
            CreateMap<Image, ImageDto>();
            CreateMap<ImageDto, Image>();
        }
    }

    public class SettingsService : TPPDbService, ISettingsService
    {
        private IDbConnection _db;

        public SettingsService(BaseDbContext context) : base(context)
        {
            _db = context.DbConnection;
            RegisterEntities();
        }

        public SettingsService(TPPContext context) : base(context)
        {
            _db = context?.TppDbConnection;
            RegisterEntities();
        }

        public void RegisterEntities()
        {
            OrmConfiguration.DefaultDialect = SqlDialect.MsSql;

            //Initialize static mapping
            AutoMapperConfiguration.Configure();
        }
        public async Task<IList<PlayerPositionDto>> GetPlayerPositions()
        {
            var playerPositionsDb = await _db.FindAsync<Position>();

            return playerPositionsDb.Select(rec =>
                Mapper.Map<PlayerPositionDto>(rec)).ToList();
        }

        public async Task<int> CreatePlayerPosition(PlayerPositionDto playerPositionDto)
        {
            //Map to the Data Entity object
            var recDb = Mapper.Map<Position>(playerPositionDto);

            //Insert into Message table
            await _db.InsertAsync(recDb);

            return recDb.Id;
        }

        public async Task<bool> UpdatePlayerPosition(PlayerPositionDto playerPositionDto)
        {
            //Map to the DB object
            var newRec = Mapper.Map<Position>(playerPositionDto);

            //Update the database
            return await _db.UpdateAsync(newRec);
        }

        public async Task<bool> DeletePlayerPosition(int playerPositionId)
        {
            //Get the questionnaire by its Id
            var dbRec = await _db.GetAsync(new Position() { Id = playerPositionId });
            //Delete the the record from the DB
            return await _db.DeleteAsync(dbRec);
        }


        public async Task<IList<SubPositionDto>> GetSubPositions()
        {
            var subPositionsDb = await _db.FindAsync<SubPosition>();

            return subPositionsDb.Select(rec =>
                Mapper.Map<SubPositionDto>(rec)).ToList();
        }

        public async Task<int> CreateSubPosition(SubPositionDto subPositionDto)
        {
            //Map to the Data Entity object
            var recDb = Mapper.Map<SubPosition>(subPositionDto);

            //Insert into Message table
            await _db.InsertAsync(recDb);

            return recDb.Id;
        }

        public async Task<bool> UpdateSubPosition(SubPositionDto subPositionDto)
        {
            //Map to the DB object
            var newRec = Mapper.Map<SubPosition>(subPositionDto);

            //Update the database
            return await _db.UpdateAsync(newRec);
        }

        public async Task<bool> DeleteSubPosition(int subPositionId)
        {
            var dbRec = await _db.GetAsync(new SubPosition() { Id = subPositionId });
            //Delete the the record from the DB
            return await _db.DeleteAsync(dbRec);
        }

        public async Task<IList<MesocycleDto>> GetMesocycles()
        {
            var mesocyclesDb = await _db.FindAsync<Mesocycle>();

            return mesocyclesDb.Select(rec =>
                Mapper.Map<MesocycleDto>(rec)).ToList();
        }

        public async Task<int> CreateMesocycle(MesocycleDto mesocycleDto)
        {

            //Map to the Data Entity object
            var recDb = Mapper.Map<Mesocycle>(mesocycleDto);

            //Insert into Message table
            await _db.InsertAsync(recDb);

            return recDb.Id;
        }

        public async Task<bool> UpdateMesocycle(MesocycleDto mesocycleDto)
        {
            var newRec = Mapper.Map<Mesocycle>(mesocycleDto);

            //Update the database
            return await _db.UpdateAsync(newRec);

        }

        public async Task<bool> DeleteMesocycle(int mesocycleId)
        {
            var dbRec = await _db.GetAsync(new Mesocycle() { Id = mesocycleId });
            //Delete the the record from the DB
            return await _db.DeleteAsync(dbRec);
        }

        public async Task<IList<MicrocycleDto>> GetMicrocycles()
        {
            var microcyclesDb = await _db.FindAsync<Microcycle>();

            return microcyclesDb.Select(rec =>
                Mapper.Map<MicrocycleDto>(rec)).ToList();
        }

        public async Task<int> CreateMicrocycle(MicrocycleDto microcycleDto)
        {
            //Map to the Data Entity object
            var recDb = Mapper.Map<Microcycle>(microcycleDto);

            //Insert into Message table
            await _db.InsertAsync(recDb);

            return recDb.Id;
        }

        public async Task<bool> UpdateMicrocycle(MicrocycleDto microcycleDto)
        {
            var newRec = Mapper.Map<Microcycle>(microcycleDto);

            //Update the database
            return await _db.UpdateAsync(newRec);
        }

        public async Task<bool> DeleteMicrocycle(int microcycleId)
        {
            var dbRec = await _db.GetAsync(new Microcycle() { Id = microcycleId });
            //Delete the the record from the DB
            return await _db.DeleteAsync(dbRec);
        }

        public async Task<IList<ImageDto>> GetImages()
        {
            var microcyclesDb = await _db.FindAsync<Image>();

            return microcyclesDb.Select(rec =>
                Mapper.Map<ImageDto>(rec)).ToList();
        }

        public async Task<int> CreateImage(ImageDto imageDto)
        {
            //Map to the Data Entity object
            var recDb = Mapper.Map<Image>(imageDto);

            //Insert into Message table
            await _db.InsertAsync(recDb);

            return recDb.Id;
        }

        public async Task<bool> UpdateImage(ImageDto imageDto)
        {

            var newRec = Mapper.Map<Image>(imageDto);

            //Update the database
            return await _db.UpdateAsync(newRec);
        }

        public async Task<bool> DeleteImage(int imageId)
        {

            var dbRec = await _db.GetAsync(new Image() { Id = imageId });
            //Delete the the record from the DB
            return await _db.DeleteAsync(dbRec);
        }
    }
}
