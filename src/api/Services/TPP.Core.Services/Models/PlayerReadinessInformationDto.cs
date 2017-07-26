// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

namespace TPP.Core.Services.Models
{
    public class PlayerReadinessInformationDto
    {
        public int? PlayerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CurrentReadiness { get; set; }

        public double? ReadinessTrend { get; set; }

        public string PathToPhoto { get; set; }

        public bool? IsInjured { get; set; }

        public string DominantSkill { get; set; }
    }
}
