// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPP.Core.Data.Entities
{
    [Table("Team")]
    public class Team : EntityBase
    {
        [Required]
        public string Name { get; set; }

        public int? SportId { get; set; }

        public int? CoachId { get; set; }

        public int? PlayerCaptainId { get; set; }

        public DateTime? Founded { get; set; }

        public bool? IsAffiliate { get; set; }

        public int? MinAge { get; set; }

        public int? MaxAge { get; set; }

        [Column(TypeName = "text")]
        public string Abbreviation { get; set; }

        public int? TeamId { get; set; }

        public int? LocaleId { get; set; }

        public bool? isNationalTeam { get; set; }

        public string PathtoPhoto { get; set; }

        public string Competition { get; set; }

        public string Gender { get; set; }

        public int? GroupId { get; set; }

        public string PrimaryColor { get; set; }

        public string SecondaryColor { get; set; }

        public IEnumerable<UserTeam> UserTeams { get; set; }

    }
}