// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

namespace TPP.Core.Services.Models
{
    using System;

    public class WellnessChartChunkDto
    {
        public string Label { get; set; }

        public DateTime Date { get; set; }

        public double? WellnessValue { get; set; }

        public double? TotalLoadValue { get; set; }
    }
}
