// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPP.Core.Data.Entities
{
    [Table("Soreness")]
    public class Soreness
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