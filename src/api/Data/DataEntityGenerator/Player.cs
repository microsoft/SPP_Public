namespace TPP.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Player")]
    public partial class Player
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int? JerseyNumber { get; set; }

        public int? PositionId { get; set; }

        public int? SubPositionId { get; set; }

        public int? PlayerDepth { get; set; }

        public int? DominantSkillId { get; set; }

        public bool? isWatchlist { get; set; }

        public bool isResting { get; set; }

        public bool? isInjured { get; set; }

        [Column(TypeName = "text")]
        public string Availability { get; set; }

        [Column(TypeName = "text")]
        public string BowlingGroup { get; set; }

        [Column(TypeName = "text")]
        public string BowlingHand { get; set; }

        [Column(TypeName = "text")]
        public string BattingGroup { get; set; }

        [Column(TypeName = "text")]
        public string BattingHand { get; set; }

        [Column(TypeName = "text")]
        public string IsKeeper { get; set; }
    }
}
