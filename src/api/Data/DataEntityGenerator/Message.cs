namespace TPP.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Message")]
    public partial class Message
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public DateTime Sent { get; set; }

        public int FromId { get; set; }

        public int ToId { get; set; }

        public bool IsActive { get; set; }
    }
}
