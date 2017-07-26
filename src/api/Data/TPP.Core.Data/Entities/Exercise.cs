// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPP.Core.Data.Entities
{
    [Table("Exercise")]
    public class Exercise : EntityBase
    {
        public string Category { get; set; }

        public string Description { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("Image")]
        public int? ImageId { get; set; }

        public Image Image { get; set; }
    }
}