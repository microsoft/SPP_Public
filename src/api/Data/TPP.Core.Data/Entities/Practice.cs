// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPP.Core.Data.Entities
{
    [Table("Practice")]
    public class Practice : EntityBase
    {
        [ForeignKey("Session")]
        public int SessionId { get; set; }

        public string Name { get; set; }

        public string Topic { get; set; }

        public string SubTopic { get; set; }

        public int? EstimatedTrainingLoad { get; set; }

        public int? RecommendedTrainingLoad { get; set; }

        public string Side { get; set; }

        public bool Modified { get; set; }

        [ForeignKey("Coach")]
        public int? CoachId { get; set; }

        [ForeignKey("Note")]
        public int? NoteId { get; set; }

        //Navigation properties
        public Session Session { get; set; }
        public User Coach { get; set; }
        public Note Note { get; set; }

    }
}