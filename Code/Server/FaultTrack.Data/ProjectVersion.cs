namespace FaultTrack.Data
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ProjectVersion
    {
        public ProjectVersion()
        {
            ProjectVersions = new HashSet<ProjectVersion>();
        }

        public int ProjectVersionId { get; set; }
        public int? ParentProjectVersionId { get; set; }
        public int ProjectId { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public virtual Project Project { get; set; }
        public virtual ICollection<ProjectVersion> ProjectVersions { get; set; }
        public virtual ProjectVersion ParentProjectVersion { get; set; }
    }
}
