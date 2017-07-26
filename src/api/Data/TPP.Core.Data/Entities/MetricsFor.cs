// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.ComponentModel.DataAnnotations.Schema;

namespace TPP.Core.Data.Entities
{
    [Table("MetricsFor")]
    public class MetricsFor : EntityBase
    {
        public string Name { get; set; }
    }
}