// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPP.Core.Data.Entities
{
    public class SeattleFlames_Catapult
    {
        [Key]
        [Column("_Id")]
        public Guid C_Id { get; set; }

        public string Name { get; set; }

        public string PositionName { get; set; }

        public double? MaximumVelocity { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? TotalDuration { get; set; }

        public double? TotalPlayerLoad { get; set; }

        public double? TotalDistance { get; set; }

        public double? HIDistanceAverage { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? Time90HRmax { get; set; }

        public double? HIDistance { get; set; }

        public int? MaxSprints { get; set; }

        public int? HIRuns { get; set; }

        [Column("_CreatedDateTimeUtc", TypeName = "datetime2")]
        public DateTime C_CreatedDateTimeUtc { get; set; }

        [Column("_LastModifiedDateTimeUtc", TypeName = "datetime2")]
        public DateTime C_LastModifiedDateTimeUtc { get; set; }

        [Column("_ImportId")]
        public string C_ImportId { get; set; }

        [Column("_Hash")]
        public string C_Hash { get; set; }
    }
}