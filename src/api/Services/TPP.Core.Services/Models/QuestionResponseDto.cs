// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;

namespace TPP.Core.Services.Models
{
	public sealed class QuestionResponseDto
	{
        public int QuestionId { get; set; }
        public DateTime AnswerDateTime { get; set; }
        public KeyValuePair<string, int> Answer { get; set; }

    }
}