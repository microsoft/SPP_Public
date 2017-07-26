// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;

namespace TPP.Core.Services.Models
{
    public class UserIdentityDto
    {
        public double Confidence { get; set; }
        public string FullName { get; set; }
        public EmotionsDto Emotions { get; set; }
    }
}