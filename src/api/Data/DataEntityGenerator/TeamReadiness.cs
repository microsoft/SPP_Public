namespace TPP.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TeamReadiness")]
    public partial class TeamReadiness
    {
        public int Id { get; set; }

        public int? TeamId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? TrackedDate { get; set; }

        public int? Value { get; set; }
    }
}
