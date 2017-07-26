namespace TPP.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Session")]
    public partial class Session
    {
        public int Id { get; set; }

        public int Type { get; set; }

        public DateTime StartTime { get; set; }

        public int? LocationId { get; set; }
    }
}
