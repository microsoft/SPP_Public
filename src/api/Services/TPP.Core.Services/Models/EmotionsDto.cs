// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;

namespace TPP.Core.Services.Models
{
	public class EmotionsDto : ModelBase
	{
        public float Anger { get; set; }
        public float Contempt { get; set; }
        public float Disgust { get; set; }
        public float Fear { get; set; }
        public float Happiness { get; set; }
        public float Neutral { get; set; }
        public float Sadness { get; set; }
        public float Surprise { get; set; }
        public DateTime CapturedOn { get; set; }

        //Navigation keys
        public int UserId { get; set; }
	    public int TeamId { get; set; }

    }
}