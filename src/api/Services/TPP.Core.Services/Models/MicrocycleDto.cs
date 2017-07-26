// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;

namespace TPP.Core.Services.Models
{
	public class MicrocycleDto: ModelBase
	{
        public string Name { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? TeamId { get; set; }

        public int? SubTeamId { get; set; }

        public int? SeasonId { get; set; }
    }
}