// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPP.Core.Data.Entities
{
    [Table("PlayerSession")]
    public class PlayerSession : EntityBase
    {
        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public int? PlayerId { get; set; }

        public string NoTrainReason { get; set; }
    }
}