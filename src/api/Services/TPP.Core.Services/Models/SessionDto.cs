// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;

namespace TPP.Core.Services.Models
{
	public sealed class SessionDto : ModelBase
	{
        public string SessionType { get; set; }
        public DateTime Scheduled { get; set; }
        public LocationDto Location { get; set; }
        public IEnumerable<UserDto> Users { get; set; }

    }
}