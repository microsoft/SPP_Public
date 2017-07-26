namespace TPP.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MetricsFor")]
    public partial class MetricsFor
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
