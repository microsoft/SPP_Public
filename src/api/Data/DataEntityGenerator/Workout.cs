namespace TPP.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Workout")]
    public partial class Workout
    {
        public int Id { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Name { get; set; }

        [Column(TypeName = "text")]
        public string Topic { get; set; }

        [Column(TypeName = "text")]
        public string SubTopic { get; set; }

        [Column(TypeName = "text")]
        public string Category { get; set; }
    }
}
