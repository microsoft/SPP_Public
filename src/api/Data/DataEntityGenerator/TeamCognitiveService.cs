namespace TPP.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TeamCognitiveService
    {
        public int Id { get; set; }

        public int? TeamId { get; set; }

        public int? CognitiveServiceId { get; set; }
    }
}
