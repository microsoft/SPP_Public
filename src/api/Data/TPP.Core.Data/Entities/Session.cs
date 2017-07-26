// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPP.Core.Data.Entities
{
    [Table("Session")]
    public class Session : EntityBase
    {
        public int Type { get; set; }

        public DateTime StartTime { get; set; }

        [ForeignKey("Location")]
        public int? LocationId { get; set; }

        public Location Location { get; set; }
    }
}