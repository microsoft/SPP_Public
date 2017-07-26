// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPP.Core.Data.Entities
{
    public class TeamMatch : EntityBase
    {
        [ForeignKey("FirstTeam")]
        public int? FirstTeamId { get; set; }

        [ForeignKey("FirstTeam")]
        public int? SecondTeamId { get; set; }

        public DateTime? DateTime { get; set; }

        [ForeignKey("Location")]
        public int? LocationId { get; set; }

        public int? FirstTeamScore { get; set; }

        public int? SecondTeamScore { get; set; }

        //marks a foreign key and also points to the property holding the related entity
        public Team FirstTeam { get; set; }
        public Team SecondTeam { get; set; }
        public Location Location { get; set; }

    }
}