namespace TPP.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Practice")]
    public partial class Practice
    {
        public int Id { get; set; }

        public int SessionId { get; set; }

        [Column(TypeName = "text")]
        public string Name { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Topic { get; set; }

        [Column(TypeName = "text")]
        public string SubTopic { get; set; }

        public int? EstimatedTrainingLoad { get; set; }

        public int? RecommendedTrainingLoad { get; set; }

        [Column(TypeName = "text")]
        public string Side { get; set; }

        public bool Modified { get; set; }

        public int? CoachId { get; set; }

        public int? NoteId { get; set; }
    }
}
