// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPP.Core.Data.Entities
{
    [Table("TeamReadiness")]
    public class TeamReadiness : EntityBase
    {
        public int? TeamId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? TrackedDate { get; set; }

        public int? Value { get; set; }
    }
}