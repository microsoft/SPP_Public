// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.ComponentModel.DataAnnotations.Schema;

namespace TPP.Core.Data.Entities
{
    [Table("UserQuestionnaire")]
    public class UserQuestionnaire : EntityBase
    {
        public int? UserId { get; set; }

        public int? QuestionnaireId { get; set; }

        public int? StatusId { get; set; }
    }
}