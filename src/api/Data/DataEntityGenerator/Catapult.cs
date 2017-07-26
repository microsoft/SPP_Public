namespace TPP.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Catapult")]
    public partial class Catapult
    {
        [Key]
        [Column("_Id")]
        public Guid C_Id { get; set; }

        public string FullName { get; set; }

        public string Activity { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? Date { get; set; }

        public string Position_Name { get; set; }

        public double? Maximum_Velocity_kmh { get; set; }

        public double? Total_Duration { get; set; }

        public double? Total_Player_Load { get; set; }

        public double? Total_Distance_m { get; set; }

        public double? HI_Distance_avg_m { get; set; }

        public double? Time90Percent_HRmax { get; set; }

        public double? HI_Distance_m { get; set; }

        public int? Max_Sprints { get; set; }

        public int? HI_Runs { get; set; }

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
