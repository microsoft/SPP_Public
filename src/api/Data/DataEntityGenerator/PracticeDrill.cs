namespace TPP.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PracticeDrill")]
    public partial class PracticeDrill
    {
        public int Id { get; set; }

        public int PracticeId { get; set; }

        public int DrillId { get; set; }

        public bool IsModified { get; set; }

        public int? Duration { get; set; }

        [Column(TypeName = "text")]
        public string Size { get; set; }

        public int? NumberOfPlayers { get; set; }

        public int Sequence { get; set; }

        public int? CalculatedTrainingLoad { get; set; }

        public int? NoteId { get; set; }
    }
}
