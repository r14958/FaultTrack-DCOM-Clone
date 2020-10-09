namespace FaultTrack.Data
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ProjectCollection
    {
        public ProjectCollection()
        {
            Projects = new HashSet<Project>();
        }

        public int ProjectCollectionId { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
