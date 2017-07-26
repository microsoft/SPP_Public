// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

namespace TPP.Core.Services.Models
{
    using System;
    using System.Collections.Generic;

    public class MatchInfoDto
    {
        public int? FirstTeamId { get; set; }

        public int? SecondTeamId { get; set; }

        public DateTime? DateTime { get; set; }

        public int? LocationId { get; set; }

        public int? FirstTeamScore { get; set; }

        public int? SecondTeamScore { get; set; }

        public TeamDto FirstTeam { get; set; }

        public TeamDto SecondTeam { get; set; }

        public LocationDto Location { get; set; }
    }
}