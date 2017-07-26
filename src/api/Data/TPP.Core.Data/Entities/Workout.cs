// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPP.Core.Data.Entities
{
    [Table("Workout")]
    public class Workout : EntityBase
    {
        [Column(TypeName = "text")]
        [Required]
        public string Name { get; set; }

        [Column(TypeName = "text")]
        public string Topic { get; set; }

        [Column(TypeName = "text")]
        public string SubTopic { get; set; }

        [Column(TypeName = "text")]
        public string Category { get; set; }
    }
}