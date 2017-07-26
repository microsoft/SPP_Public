namespace TPP.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WorkoutData")]
    public partial class WorkoutData
    {
        public int Id { get; set; }

        public int? SessionId { get; set; }

        public int? NoteId { get; set; }

        public int? Duration { get; set; }

        public int? WorkoutId { get; set; }
    }
}
