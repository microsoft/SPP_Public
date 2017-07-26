namespace TPP.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QuestionnaireResponse")]
    public partial class QuestionnaireResponse
    {
        public int Id { get; set; }

        public int? QuestionnaireId { get; set; }

        public int? QuestionId { get; set; }

        public int? UserId { get; set; }

        public decimal? Value { get; set; }

        public DateTime? AnswerDateTime { get; set; }
    }
}
