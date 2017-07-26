// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

namespace TPP.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("InjuryData")]
    public partial class InjuryData : EntityBase
    {
        [Column(TypeName = "datetime2")]
        public DateTime? Date { get; set; }

        public string PlayerName { get; set; }

        public string InjuryType { get; set; }

        public string Location { get; set; }

        public int? Severity { get; set; }

        public int? isInjured { get; set; }

        public string Notes { get; set; }
    }
}
