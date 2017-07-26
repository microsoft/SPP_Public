namespace TPP.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SubTeam")]
    public partial class SubTeam
    {
        public int Id { get; set; }

        public int TeamId { get; set; }

        [StringLength(10)]
        public string Gender { get; set; }

        [Column(TypeName = "text")]
        public string Name { get; set; }

        public int? GameMinutes { get; set; }

        [Column(TypeName = "text")]
        public string Abbreviation { get; set; }

        public int? LeagueId { get; set; }

        public int? CoachId { get; set; }

        public int? PlayerCaptainId { get; set; }
    }
}
