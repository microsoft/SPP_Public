// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPP.Core.Data.Entities
{
    [Table("Position")]
    public class Position : EntityBase
    {
        [Column(TypeName = "text")]
        [Required]
        public string Name { get; set; }

        public int? SportId { get; set; }

        [Column(TypeName = "text")]
        public string Abbreviation { get; set; }
    }
}