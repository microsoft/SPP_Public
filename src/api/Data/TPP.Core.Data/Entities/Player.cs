// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.ComponentModel.DataAnnotations.Schema;

namespace TPP.Core.Data.Entities
{
    [Table("Player")]
    public class Player : EntityBase
    {
        [ForeignKey("User")]
        public int UserId { get; set; }

        public int? JerseyNumber { get; set; }

        public int? PositionId { get; set; }

        public int? SubPositionId { get; set; }

        public int? PlayerDepth { get; set; }

        [ForeignKey("DominantSkill")]
        public int? DominantSkillId { get; set; }

        public bool? isWatchlist { get; set; }

        public bool isResting { get; set; }

        public bool? isInjured { get; set; }

        public string Availability { get; set; }

        public string BowlingGroup { get; set; }

        public string BowlingHand { get; set; }

        public string BattingGroup { get; set; }

        public string BattingHand { get; set; }

        public string IsKeeper { get; set; }


        public User User { get; set; }
        public Skill DominantSkill { get; set; }
    }
}