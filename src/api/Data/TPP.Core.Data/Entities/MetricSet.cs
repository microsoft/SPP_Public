// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.ComponentModel.DataAnnotations.Schema;

namespace TPP.Core.Data.Entities
{
    [Table("MetricSet")]
    public class MetricSet : EntityBase
    {
        public string ShortName { get; set; }

        public string FullName { get; set; }

        public string Tooltip { get; set; }
    }
}