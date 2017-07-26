// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.ComponentModel.DataAnnotations.Schema;

namespace TPP.Core.Data.Entities
{
    public class QuestionnaireStatu : EntityBase
    {
        [Column(TypeName = "text")]
        public string Name { get; set; }
    }
}