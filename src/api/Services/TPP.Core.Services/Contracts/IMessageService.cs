// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.Collections.Generic;
using System.Threading.Tasks;
using TPP.Core.Services.Models;

namespace TPP.Core.Services.Contracts
{
    public interface IMessageService : IEntityConfiguration
    {
        Task<List<MessageDto>> GetMessages(int playerId);

        Task<int> CreateMessage(MessageDto messageDto);

        Task<MessageDto> GetMessage(int messageId);

        Task<bool> UpdateMessage(MessageDto messageDto);

        Task<bool> DeleteMessage(int messageId);

        Task<bool> DeletePlayerMessages(int playerId);
    }
}