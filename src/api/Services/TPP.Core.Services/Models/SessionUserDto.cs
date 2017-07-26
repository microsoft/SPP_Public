// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;

namespace TPP.Core.Services.Models
{
	public sealed class SessionUserDto : ModelBase
	{
        public int SessionId { get; set; }

        public int UserId { get; set; }

    }
}