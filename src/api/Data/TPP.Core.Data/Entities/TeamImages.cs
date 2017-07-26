// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPP.Core.Data.Entities
{
    public class TeamImages : EntityBase
    {
        [ForeignKey("Team")]
        public int TeamId { get; set; }

        [ForeignKey("Image")]
        public int ImageId { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public bool? IsActive { get; set; }

        //marks a foreign key and also points to the property holding the related entity
        public Team Team { get; set; }
        public Image Image { get; set; }

    }
}