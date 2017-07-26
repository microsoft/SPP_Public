// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

namespace TPP.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class TeamCognitiveService : EntityBase
    {
        [ForeignKey("Team")]
        public int? TeamId { get; set; }

        [ForeignKey("CognitiveService")]
        public int? CognitiveServiceId { get; set; }

        //marks a foreign key and also points to the property holding the related entity
        public Team Team { get; set; }
        public CognitiveService CognitiveService { get; set; }
    }
}
