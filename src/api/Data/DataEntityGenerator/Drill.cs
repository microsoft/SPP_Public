namespace TPP.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Drill")]
    public partial class Drill
    {
        public int Id { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Name { get; set; }

        public int? ImageId { get; set; }

        [Column(TypeName = "text")]
        public string Category { get; set; }

        [Column(TypeName = "text")]
        public string SubCategory { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }
    }
}
