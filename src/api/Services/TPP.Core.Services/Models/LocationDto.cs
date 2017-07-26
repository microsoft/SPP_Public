// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

namespace TPP.Core.Services.Models
{
	public class LocationDto: ModelBase
	{
		public string Name { get; set; }
		public string Address { get; set; }
		public LocationType Type { get; set; }
	}
}