// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using TPP.Core.Services.Models;

namespace TPP.Core.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;


    /// <summary>This service provides methods for working with data for Team API controller. </summary>
    public interface ITeamService : IEntityConfiguration
    {
        /// <summary>This method gets domestic Team and players. </summary>
        /// <param name="id">Team Id. </param>
        /// <returns>Team model. </returns>
        Task<TeamDto> GetDomesticTeamPlayers(int id);

        // <summary>This method gets ALL Team and players. </summary>
        /// <returns>Team model. </returns>
        Task<TeamDto> GetAllTeamPlayers();

        /// <summary> This method provides data of Team Readiness for last 6 months and for current month. </summary>
        /// <param name="id">Team Id. </param>
        /// <returns>List of Team Readiness entities</returns>
        Task<IEnumerable<MonthReadinessDto>> GetTeamReadiness(int id);

        /// <summary>This methods provides information about readiness stats of all player of Team. </summary>
        /// <param name="id">Team Id. </param>
        /// <returns>List of PlayerReadinessInformationDto entities. </returns>
        Task<IEnumerable<PlayerReadinessInformationDto>> GetPlayersReadiness(int id);

        /// <summary>This methods provides information about next match. </summary>
        /// <param name="teamId">Team Id. </param>
        /// <returns>MatchInfoDto entity. </returns>
        Task<MatchInfoDto> GetNextMatchInfo(int teamId, DateTime currentDate);


        /// <summary>This methods provides information about next match. </summary>
        /// <param name="teamId">Team Id. </param>
        /// <returns>MatchInfoDto entity. </returns>
        Task<IEnumerable<MatchInfoDto>> GetMatchHistoryInfo(int teamId);


        Task<int> CreateTeam(TeamDto teamDto);

        Task<IList<TeamDto>> GetTeams();

        Task<TeamDto> GetTeam(int teamId);
        Task<TeamDto> GetTeamByName(string teamName);

        Task<bool> UpdateTeam(TeamDto teamDto);

        Task<bool> DeleteTeam(int teamId);

        Task<bool> AddTeamImages(TeamImagesDto teamImages);
        Task<TeamImagesDto> GetTeamImages(int teamId);

    }
}
