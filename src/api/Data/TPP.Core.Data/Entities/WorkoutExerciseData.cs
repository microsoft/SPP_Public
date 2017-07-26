// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.ComponentModel.DataAnnotations.Schema;

namespace TPP.Core.Data.Entities
{
    [Table("WorkoutExerciseData")]
    public class WorkoutExerciseData : EntityBase
    {
        [ForeignKey("Workout")]
        public int WorkoutId { get; set; }

        [ForeignKey("WorkoutExercise")]
        public int WorkoutExerciseId { get; set; }

        public int? Sets { get; set; }

        public int? Reps { get; set; }

        public float? Weight { get; set; }

        public int? RecoveryTimeInMin { get; set; }

        [ForeignKey("Message")]
        public int? MessageId { get; set; }

        public bool? isCompleted { get; set; }

        public bool? isModified { get; set; }

        [ForeignKey("Note")]
        public int? NoteId { get; set; }


        //Navigation properties
        public Workout Workout { get; set; }
        public WorkoutExercise WorkoutExercise { get; set; }
        public Message Message { get; set; }
        public Note Note { get; set; }
    }
}