// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.Collections.Generic;

namespace TPP.Core.Services.Models
{
    public class SubPositionDto : ModelBase
    {
       
        public string Name { get; set; }

        public int PositionId { get; set; }

        
        public string Abbreviation { get; set; }
    }
}