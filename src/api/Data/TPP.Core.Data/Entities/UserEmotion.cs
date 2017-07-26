// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

namespace TPP.Core.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UserEmotion")]
    public partial class UserEmotion : EntityBase
    {
        public int UserId { get; set; }

        public double Anger { get; set; }

        public double Contempt { get; set; }

        public double Disgust { get; set; }

        public double Fear { get; set; }

        public double Happiness { get; set; }

        public double Neutral { get; set; }

        public double Sadness { get; set; }

        public double Surprise { get; set; }

        public DateTime TakenOn { get; set; }
    }
}
