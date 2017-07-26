// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.Collections.Generic;

namespace TPP.Core.Services.Models
{
    public class PlayerInfoDto : ModelBase
    {
        public int? Depth { get; set; }

        public string DominantSkill { get; set; }

        public List<RestrictionDto> Restrictions { get; set; }

        public bool? IsResting { get; set; }
    }
}