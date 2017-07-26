// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.ComponentModel.DataAnnotations.Schema;

namespace TPP.Core.Data.Entities
{
    [Table("SeasonSubTeam")]
    public class SeasonSubTeam : EntityBase
    {
        public int SubTeamId { get; set; }

        public int SeasonId { get; set; }
    }
}