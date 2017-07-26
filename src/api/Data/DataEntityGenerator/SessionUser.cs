namespace TPP.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SessionUser")]
    public partial class SessionUser
    {
        public int Id { get; set; }

        public int SessionId { get; set; }

        public int UserId { get; set; }
    }
}
