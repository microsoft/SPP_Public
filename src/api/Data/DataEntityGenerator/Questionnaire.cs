namespace TPP.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Questionnaire")]
    public partial class Questionnaire
    {
        public int Id { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public int? SequenceOrder { get; set; }

        public bool? isEnabled { get; set; }

        public int? SessionId { get; set; }

        public int? MesocycleId { get; set; }

        public int? MicrocycleId { get; set; }

        public int? DayTypeId { get; set; }
    }
}
