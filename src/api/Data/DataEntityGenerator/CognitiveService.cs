namespace TPP.Core.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CognitiveService")]
    public partial class CognitiveService
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string WorkspaceKey { get; set; }

        [Required]
        [StringLength(50)]
        public string FaceApiKey { get; set; }

        [Required]
        [StringLength(50)]
        public string EmotionApiKey { get; set; }

        [StringLength(50)]
        public string CameraName { get; set; }

        public int MinDetectableFaceCoveragePercentage { get; set; }
    }
}
