// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using TPP.Core.Services.Models;

namespace TPP.Core.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISettingsService : IEntityConfiguration
    {
        Task<IList<PlayerPositionDto>> GetPlayerPositions();
        Task<int> CreatePlayerPosition(PlayerPositionDto playerPositionDto);
        Task<bool> UpdatePlayerPosition(PlayerPositionDto playerPositionDto);
        Task<bool> DeletePlayerPosition(int playerPositionId);

        Task<IList<SubPositionDto>> GetSubPositions();
        Task<int> CreateSubPosition(SubPositionDto subPositionDto);
        Task<bool> UpdateSubPosition(SubPositionDto subPositionDto);
        Task<bool> DeleteSubPosition(int subPositionId);

        Task<IList<MesocycleDto>> GetMesocycles();
        Task<int> CreateMesocycle(MesocycleDto mesocycleDto);
        Task<bool> UpdateMesocycle(MesocycleDto mesocycleDto);
        Task<bool> DeleteMesocycle(int mesocycleId);

        Task<IList<MicrocycleDto>> GetMicrocycles();
        Task<int> CreateMicrocycle(MicrocycleDto microcycleDto);
        Task<bool> UpdateMicrocycle(MicrocycleDto microcycleDto);
        Task<bool> DeleteMicrocycle(int microcycleId);


        Task<IList<ImageDto>> GetImages();
        Task<int> CreateImage(ImageDto imageDto);
        Task<bool> UpdateImage(ImageDto imageDto);
        Task<bool> DeleteImage(int imageId);
    }
}
