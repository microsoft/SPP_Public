// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.Collections.Generic;

namespace TPP.Core.Services.Models
{
    public class AthletePracticeDto 
    {
        public string Topic { get; set; }
        public string SubTopic { get; set; }
        public int SessionId { get; set; }
        public int? EstimatedTrainingLoad { get; set; }
        public List<AthleteDrillDto> Drills { get; set; }
    }
}