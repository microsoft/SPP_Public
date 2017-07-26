// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;

namespace TPP.Core.Services.Models
{
	public class PracticeDto : ModelBase
	{
        public int SessionId { get; set; }

        public string Name { get; set; }

        public string Topic { get; set; }

        public string SubTopic { get; set; }

        public int? EstimatedTrainingLoad { get; set; }

        public int? RecommendedTrainingLoad { get; set; }

        public string Side { get; set; }

	    public int TeamId { get; set; }

        public bool IsModified { get; set; }

        public int? CoachId { get; set; }

	    public int? NoteId { get; set; }


        //Navigation Entities
        public IList<PracticeDrillDto> PracticeDrills { get; set; }
        
        public NoteDto Note { get; set; }
	    public UserDto Coach { get; set; }
	    public SessionDto Session { get; set; }
    }
}