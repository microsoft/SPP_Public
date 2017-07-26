// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

namespace TPP.Core.Services.Models
{
    using System.Collections.Generic;

    public class MetricSetDto
    {
        public int Id { get; set; }

        public string ShortName { get; set; }

        public string FullName { get; set; }

        public string Tooltip { get; set; }

        public List<MetricDto> Metrics { get; set; }
    }
}