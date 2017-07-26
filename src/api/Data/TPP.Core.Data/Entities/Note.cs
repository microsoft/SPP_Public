// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPP.Core.Data.Entities
{
    [Table("Note")]
    public class Note : EntityBase
    {
        [Required]
        public string Text { get; set; }

        public DateTime Created { get; set; }
    }
}