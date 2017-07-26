namespace TPP.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Team")]
    public partial class Team
    {
        public int Id { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Name { get; set; }

        public int? SportId { get; set; }

        public int? CoachId { get; set; }

        public int? PlayerCaptainId { get; set; }

        public DateTime? Founded { get; set; }

        public bool? IsAffiliate { get; set; }

        public int? MinAge { get; set; }

        public int? MaxAge { get; set; }

        [Column(TypeName = "text")]
        public string Abbreviation { get; set; }

        public int? TeamId { get; set; }

        public int? LocaleId { get; set; }

        public bool? isNationalTeam { get; set; }

        public string PathtoPhoto { get; set; }

        public string Competition { get; set; }

        public string Gender { get; set; }

        public int? GroupId { get; set; }
    }
}
