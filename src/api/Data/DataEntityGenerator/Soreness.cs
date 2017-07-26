namespace TPP.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Soreness")]
    public partial class Soreness
    {
        [Key]
        [Column("_Id")]
        public Guid C_Id { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? Date { get; set; }

        public DateTimeOffset? Time { get; set; }

        public string FullName { get; set; }

        public string Site { get; set; }

        public int? Rating { get; set; }

        public string Descriptor { get; set; }

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
