// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.ComponentModel.DataAnnotations.Schema;

namespace TPP.Core.Data.Entities
{
    [Table("QuestionnaireQuestion")]
    public class QuestionnaireQuestion : EntityBase
    {
        [ForeignKey("Question")]
        public int QuestionId { get; set; }

        [ForeignKey("Questionnaire")]
        public int QuestionnaireId { get; set; }

        //Navigation properties
        public Question Question { get; set; }
        public Questionnaire Questionnaire { get; set; }

    }
}