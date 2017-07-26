namespace TPP.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InjuryData")]
    public partial class InjuryData
    {
        public int Id { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? Date { get; set; }

        public string PlayerName { get; set; }

        public string InjuryType { get; set; }

        public string Location { get; set; }

        public int? Severity { get; set; }

        public int? isInjured { get; set; }

        public string Notes { get; set; }
    }
}
