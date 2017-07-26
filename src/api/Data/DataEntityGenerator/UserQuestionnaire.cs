namespace TPP.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserQuestionnaire")]
    public partial class UserQuestionnaire
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public int? QuestionnaireId { get; set; }

        public int? StatusId { get; set; }
    }
}
