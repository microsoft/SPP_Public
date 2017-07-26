// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.Collections.Generic;

namespace TPP.Core.Services.Models
{
	public class AthleteExerciseDto: ModelBase
	{
	    public int ExerciseDataId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public AthleteExerciseSetDto Sets { get; set; }
        public bool IsDone { get; set; }
        public bool IsModified { get; set; }
        public int Duration { get; set; }
        public NoteDto Note { get; set; }
        public string TrainingLoad { get; set; }
    }
}