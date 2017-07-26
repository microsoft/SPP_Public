// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.ComponentModel.DataAnnotations.Schema;

namespace TPP.Core.Data.Entities
{
    [Table("WorkoutData")]
    public class WorkoutData : EntityBase
    {
        [ForeignKey("Session")]
        public int? SessionId { get; set; }

        [ForeignKey("Note")]
        public int? NoteId { get; set; }

        public int? Duration { get; set; }

        [ForeignKey("Workout")]
        public int? WorkoutId { get; set; }

        //Navigation properties
        public Session Session { get; set; }
        public Note Note { get; set; }
        public Workout Workout { get; set; }
    }
}