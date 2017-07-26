namespace TPP.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Question")]
    public partial class Question
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Text { get; set; }

        [StringLength(50)]
        public string MinCaption { get; set; }

        [StringLength(50)]
        public string MidCaption { get; set; }

        [StringLength(50)]
        public string MaxCaption { get; set; }

        public int? MinValue { get; set; }

        public int? MidValue { get; set; }

        public int? MaxValue { get; set; }

        public int? QuestionnaireId { get; set; }
    }
}
