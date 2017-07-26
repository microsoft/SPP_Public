// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TPP.Core.Services.Models;

namespace TPP.Core.Services.Contracts
{
    public interface ISessionService : IEntityConfiguration
    {
        Task<List<SessionDto>> GetSessionsByDate(DateTime sessionDate);
        Task<List<SessionDto>> GetSessionsRange(DateTime fromDate, DateTime toDate);

        Task<int> CreateSession(SessionDto sessionDto);

        Task<bool> CreateSessionUser(SessionUserDto sessionUserDto);

        Task<SessionDto> GetSession(int sessionId);


        Task<bool> UpdateSession(SessionDto sessionDto);

        Task<bool> DeleteSession(int sessionId);

        Task AddUserToSession(int sessionId, int userId);

        Task RemoveUserFromSession(int sessionId, int userId);

    }
}