// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.ComponentModel.DataAnnotations.Schema;

namespace TPP.Core.Data.Entities
{
    [Table("PracticeDrill")]
    public class PracticeDrill : EntityBase
    {
        [ForeignKey("Practice")]
        public int PracticeId { get; set; }

        [ForeignKey("Drill")]
        public int DrillId { get; set; }

        [ForeignKey("Note")]
        public int? NoteId { get; set; }

        public bool IsModified { get; set; }

        public int? Duration { get; set; }

        public string Size { get; set; }

        public int? NumberOfPlayers { get; set; }

        public int Sequence { get; set; }

        public int? CalculatedTrainingLoad { get; set; }

        //Navigation properties
        public Practice Practice { get; set; }
        public Drill Drill { get; set; }
        public Note Note { get; set; }

    }
}