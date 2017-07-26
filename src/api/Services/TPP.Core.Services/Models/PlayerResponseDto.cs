// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.Collections.Generic;

namespace TPP.Core.Services.Models
{
    public class PlayerResponseDto
    {
        public int SessionId { get; set; }
        public int PlayerId { get; set; }
        public int QuestionnaireId { get; set; }
        public List<QuestionResponseDto> Answers { get; set; }

    }
}