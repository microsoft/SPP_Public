namespace TPP.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MatchDayType")]
    public partial class MatchDayType
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
    }
}
