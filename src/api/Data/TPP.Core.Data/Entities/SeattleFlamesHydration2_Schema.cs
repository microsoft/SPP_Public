// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

namespace TPP.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class SeattleFlamesHydration2_Schema
    {
        [Key]
        [Column("_Id")]
        public Guid C_Id { get; set; }

        public string Date { get; set; }

        public string Last { get; set; }

        public string First { get; set; }

        public string Score { get; set; }

        public string LabelText { get; set; }

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
