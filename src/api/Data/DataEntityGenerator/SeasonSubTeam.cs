namespace TPP.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SeasonSubTeam")]
    public partial class SeasonSubTeam
    {
        public int Id { get; set; }

        public int SubTeamId { get; set; }

        public int SeasonId { get; set; }
    }
}
