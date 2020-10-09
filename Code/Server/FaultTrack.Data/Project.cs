namespace FaultTrack.Data
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Project
    {
        public Project()
        {
            Projects = new HashSet<Project>();
            ProjectVersions = new HashSet<ProjectVersion>();
        }
        
        public int ProjectId { get; set; }
        public int? ParentProjectId { get; set; }
        public int ProjectCollectionId { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
        [ForeignKey(nameof(ParentProjectId))]
        public virtual Project ParentProject { get; set; }
        public virtual ProjectCollection ProjectCollection { get; set; }
        public virtual ICollection<ProjectVersion> ProjectVersions { get; set; }
    }
}
