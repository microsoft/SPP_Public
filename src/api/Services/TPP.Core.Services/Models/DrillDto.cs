// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;

namespace TPP.Core.Services.Models
{
	public class DrillDto : ModelBase
	{
		public string Name { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Description { get; set; }
        public ImageDto Image { get; set; }
    }
}