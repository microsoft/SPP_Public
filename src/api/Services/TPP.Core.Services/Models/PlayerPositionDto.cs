// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

namespace TPP.Core.Services.Models
{
    public class PlayerPositionDto : ModelBase
    {
        public string Name { get; set; }
        public int? SportId { get; set; }
        public string Abbreviation { get; set; }
    }
}