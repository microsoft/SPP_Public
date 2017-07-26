// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.ComponentModel.DataAnnotations.Schema;

namespace TPP.Core.Data.Entities
{
    [Table("WorkoutExercise")]
    public class WorkoutExercise : EntityBase
    {
        [ForeignKey("Exercise")]
        public int ExerciseId { get; set; }

        public int SequenceNumber { get; set; }

        public Exercise Exercise { get; set; }
    }
}