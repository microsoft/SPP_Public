namespace TPP.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WorkoutExercise")]
    public partial class WorkoutExercise
    {
        public int Id { get; set; }

        public int ExerciseId { get; set; }

        public int SequenceNumber { get; set; }
    }
}
