// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;

namespace TPP.Core.Services.Models
{
    public sealed class TeamImagesDto : ModelBase
    {
        public int TeamId { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public bool? IsActive { get; set; }

        public IEnumerable<ImageDto> Images { get; set; }

    }
}