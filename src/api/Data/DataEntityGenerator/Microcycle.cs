namespace TPP.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Microcycle")]
    public partial class Microcycle
    {
        public int Id { get; set; }

        [Column(TypeName = "text")]
        public string Name { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? TeamId { get; set; }

        public int? SubTeamId { get; set; }

        public int? SeasonId { get; set; }
    }
}
