namespace TPP.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WorkoutExerciseData")]
    public partial class WorkoutExerciseData
    {
        public int Id { get; set; }

        public int WorkoutId { get; set; }

        public int WorkoutExerciseId { get; set; }

        public int? Sets { get; set; }

        public int? Reps { get; set; }

        public decimal? Weight { get; set; }

        public int? RecoveryTimeInMin { get; set; }

        public int? MessageId { get; set; }

        public bool? isCompleted { get; set; }

        public bool? isModified { get; set; }

        public int? NoteId { get; set; }
    }
}
