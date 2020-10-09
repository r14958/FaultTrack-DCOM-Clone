namespace FaultTrack.Web
{
    using System.Collections.Generic;

    public class ProjectTreeNode
    {
        public int ProjectId { get; set; }
        public int? ParentProjectId { get; set; }
        public int ProjectCollectionId { get; set; }
        public string Name { get; set; }
        public IEnumerable<ProjectTreeNode> Projects { get; set; }
        public IEnumerable<ProjectVersionTreeNode> ProjectVersions { get; set; }

        public override string ToString()
        {
            return $"Id = {ProjectId}, Name = {Name}";
        }
    }
}