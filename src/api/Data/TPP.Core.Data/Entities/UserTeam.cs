// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPP.Core.Data.Entities
{
    [Table("UserTeam")]
    public class UserTeam : EntityBase
    {
        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Team")]
        public int TeamId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateStart { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateEnd { get; set; }

        //marks a foreign key and also points to the property holding the related entity
        public User User { get; set; }
        public Team Team { get; set; }

    }
}