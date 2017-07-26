namespace TPP.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SeattleFlames_Readiness
    {
        [Key]
        [Column("_Id")]
        public Guid C_Id { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? Date { get; set; }

        public DateTimeOffset? Time { get; set; }

        public string Last { get; set; }

        public string First { get; set; }

        public double? Score { get; set; }

        public int? Fatigue { get; set; }

        public int? Mood { get; set; }

        public int? Soreness { get; set; }

        public int? Stress { get; set; }

        public int? Sleep_Quality { get; set; }

        public double? Sleep_Hours { get; set; }

        [Column("_CreatedDateTimeUtc", TypeName = "datetime2")]
        public DateTime C_CreatedDateTimeUtc { get; set; }

        [Column("_LastModifiedDateTimeUtc", TypeName = "datetime2")]
        public DateTime C_LastModifiedDateTimeUtc { get; set; }

        [Column("_ImportId")]
        public string C_ImportId { get; set; }

        [Column("_Hash")]
        public string C_Hash { get; set; }
    }
}
