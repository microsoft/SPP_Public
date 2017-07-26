// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPP.Core.Data.Entities
{
    [Table("QuestionnaireResponse")]
    public class QuestionnaireResponse : EntityBase
    {
        [ForeignKey("Questionnaire")]
        public int? QuestionnaireId { get; set; }

        [ForeignKey("Question")]
        public int? QuestionId { get; set; }

        public int? UserId { get; set; }

        public decimal? Value { get; set; }

        public DateTime? AnswerDateTime { get; set; }

        //Navigation properties
        public Question Question { get; set; }
        public Questionnaire Questionnaire { get; set; }

    }
}