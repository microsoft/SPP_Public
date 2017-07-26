// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.Collections.Generic;
using System.Threading.Tasks;
using TPP.Core.Services.Models;

namespace TPP.Core.Services.Contracts
{
    public interface IPracticeService : IEntityConfiguration
    {
        //CREATE APIs
        Task<int> CreatePractice(PracticeDto practiceDto);

        //RETRIEVE APIs
        Task<PracticeDto> GetPractice(int practiceId);
        Task<IList<PracticeDto>> GetAllPractices();
        Task<IList<PracticeDto>> GetAllPracticeDetails();
        Task<IList<PracticeDto>> GetSessionPractices(int sessionId);
        Task<IList<PracticeDrillDto>> GetPracticeDrills(int practiceId);
        Task<IList<PracticeDrillDto>> GetPracticeDrills();

        //UPDATE APIs
        Task<bool> UpdatePractice(PracticeDto practiceDto);


        //DELETE APIs
        Task<bool> DeletePractice(int practiceId);

        Task<bool> DeleteAllSessionPractices(int sessionId);

    }
}