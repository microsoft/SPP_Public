using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TPP.Core.Services.Models;

namespace TPP.Core.Services.Contracts
{
    public interface ILocationService : IEntityConfiguration
    {
        Task<List<LocationDto>> GetLocations();

        Task<int> CreateLocation(LocationDto locationDto);

        Task<bool> UpdateLocatioin(LocationDto locationDto);

        Task<bool> DeleteLocation(int locationId);

    }
}