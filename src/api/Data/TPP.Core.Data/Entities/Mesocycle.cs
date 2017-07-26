// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPP.Core.Data.Entities
{
    [Table("Mesocycle")]
    public class Mesocycle : EntityBase
    {
        [Column(TypeName = "text")]
        public string Name { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? SeasonId { get; set; }

        public int? SubTeamId { get; set; }

        public int? TeamId { get; set; }

        public int? MesocycleNumber { get; set; }
    }
}