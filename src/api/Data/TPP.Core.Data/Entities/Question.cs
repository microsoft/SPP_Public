// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPP.Core.Data.Entities
{
    [Table("Question")]
    public class Question : EntityBase
    {
        [Required]
        [StringLength(50)]
        public string Text { get; set; }

        [StringLength(50)]
        public string MinCaption { get; set; }

        [StringLength(50)]
        public string MidCaption { get; set; }

        [StringLength(50)]
        public string MaxCaption { get; set; }

        public int? MinValue { get; set; }

        public int? MidValue { get; set; }

        public int? MaxValue { get; set; }

        public int? SequenceOrder { get; set; }

    }
}