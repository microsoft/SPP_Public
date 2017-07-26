// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;

namespace TPP.Core.Services.Models
{
    public class UserTeamDto
    {
        public int UserId { get; set; }
        public int TeamId { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public TeamDto Team { get; set; }
        public UserDto User { get; set; }
    }
}