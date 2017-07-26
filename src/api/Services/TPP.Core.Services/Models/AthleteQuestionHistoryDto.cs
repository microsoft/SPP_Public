// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.Collections.Generic;

namespace TPP.Core.Services.Models
{
    public class AthleteQuestionHistoryDto
    {
        public AthleteQuestionDto Question { get; set; }
        public IList<QuestionResponseDto> Responses { get; set; }
    }
}