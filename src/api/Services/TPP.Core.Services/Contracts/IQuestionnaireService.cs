// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.Collections.Generic;
using System.Threading.Tasks;
using TPP.Core.Services.Models;

namespace TPP.Core.Services.Contracts
{
    public interface IQuestionnaireService : IEntityConfiguration
    {

        //CREATE APIs
        Task<int> CreateQuestionnaire(AthleteQuestionnaireDto questionnaireDto);

        //RETRIEVE APIs
        Task<AthleteQuestionnaireDto> GetAthleteQuestions(int sessionId);
        Task<List<AthleteQuestionHistoryDto>> GetPlayerResponse(int sessionId, int playerId);

        Task<AthleteQuestionnaireDto> GetQuestionnaire(int questionnaireId);
        Task<List<AthleteQuestionnaireDto>> GetSessionQuestionnaires(int sessionId);

        //UPDATE APIs
        Task<bool> UpdateQuestionnaire(AthleteQuestionnaireDto questionnaireDto);


        //DELETE APIs
        Task<bool> DeleteQuestionnaire(int questionnaireId);

    }
}