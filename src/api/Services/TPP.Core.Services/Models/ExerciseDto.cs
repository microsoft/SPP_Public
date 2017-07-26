// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

namespace TPP.Core.Services.Models
{
    public class ExerciseDto : ModelBase
    {
        public int SequenceNumber { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public ImageDto Image { get; set; }

    }
}