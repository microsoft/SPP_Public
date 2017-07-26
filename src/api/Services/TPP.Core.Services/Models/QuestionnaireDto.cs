// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;

namespace TPP.Core.Services.Models
{
	public class QuestionnaireDto : ModelBase
	{
        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public int? SequenceOrder { get; set; }

        public bool? IsEnabled { get; set; }

        public int? SessionId { get; set; }

	    public string QuestionnaireStatus { get; set; }
    }
}