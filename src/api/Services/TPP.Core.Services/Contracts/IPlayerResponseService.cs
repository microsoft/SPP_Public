// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.Threading.Tasks;
using TPP.Core.Services.Models;

namespace TPP.Core.Services.Contracts
{
    public interface IPlayerResponseService : IEntityConfiguration
    {
        Task<bool> CreatePlayerResponse(PlayerResponseDto playerResponseDto);

        Task<PlayerResponseDto> GetPlayerResponse(PlayerResponseDto playerQuestionnaire);
        Task<bool> GetQuestionnairePlayerResponse(int playerId, int questionnaireId);

        Task<bool> UpdatePlayerResponse(PlayerResponseDto playerResponseDto);

        Task<bool> DeletePlayerResponse(int questionId);

        Task<bool> DeleteAllPlayerResponses(int playerId);
    }
}
