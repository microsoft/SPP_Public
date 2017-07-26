// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPP.Core.Data.Entities
{
    [Table("Image")]
    public class Image : EntityBase
    {
        [Required]
        public string Url { get; set; }

        public string Tags { get; set; }
    }
}