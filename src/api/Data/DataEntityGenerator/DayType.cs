namespace TPP.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DayType")]
    public partial class DayType
    {
        public int Id { get; set; }

        [Column(TypeName = "text")]
        public string Name { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Column(TypeName = "text")]
        public string RecurringDayofWeek { get; set; }

        public int? TeamId { get; set; }

        public int? SubTeamId { get; set; }
    }
}
