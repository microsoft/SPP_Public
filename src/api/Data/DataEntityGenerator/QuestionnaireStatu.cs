namespace TPP.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class QuestionnaireStatu
    {
        public int Id { get; set; }

        [Column(TypeName = "text")]
        public string Name { get; set; }
    }
}
