namespace TPP.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TeamMatch
    {
        public int Id { get; set; }

        public int? FirstTeamId { get; set; }

        public int? SecondTeamId { get; set; }

        public DateTime? DateTime { get; set; }

        public int? LocationId { get; set; }

        public int? FirstTeamScore { get; set; }

        public int? SecondTeamScore { get; set; }
    }
}
