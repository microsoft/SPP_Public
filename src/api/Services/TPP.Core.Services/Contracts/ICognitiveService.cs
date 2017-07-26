// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.Collections.Generic;
using System.Threading.Tasks;
using TPP.Core.Services.Models;

namespace TPP.Core.Services.Contracts
{
    public interface ICognitiveService : IEntityConfiguration
    {
        //CREATE APIs
        Task<bool> CreateCognitiveServiceKeys(CognitiveServiceKeysDto newKeysDto);
        Task<bool> AddUserEmotions(EmotionsDto emotionsDto);

        //RETRIEVE APIs
        Task<CognitiveServiceKeysDto> GetCognitiveServiceKeys(int teamId);
        Task<int> AuthenticateUserByFace(IList<UserIdentityDto> userIdentities);

        //UPDATE APIs
        Task<bool> UpdateCognitiveServiceKeys(CognitiveServiceKeysDto newKeysDto);

        //DELETE APIs
        Task<bool> DeleteCognitiveServiceKeys(int keyId);
    }
}