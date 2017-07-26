namespace TPP.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QuestionnaireQuestion")]
    public partial class QuestionnaireQuestion
    {
        public int Id { get; set; }

        public int QuestionId { get; set; }

        public int QuestionnaireId { get; set; }
    }
}
