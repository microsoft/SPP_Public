// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

namespace TPP.Core.Services.Models
{
    using System.Collections.Generic;

    public class MetricSubjectDataDto
    {
        public string SubjectName { get; set; }

        public List<MetricSetDataDto> Sets { get; set; }
    }

    public class MetricSetDataDto
    {
        public int Id { get; set; }

        public List<MetricDataDto> Metrics { get; set; }
    }

    public class MetricDataDto
    {
        public int Id { get; set; }

        public double? Value { get; set; }
    }
}