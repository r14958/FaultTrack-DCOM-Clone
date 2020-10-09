namespace FaultTrack.Web
{
    using System.Collections.Generic;

    public class ProjectVersionTreeNode
    {
        public int ProjectVersionId { get; set; }
        public int ProjectId { get; set; }
        public int? ParentProjectVersionId { get; set; }
        public string Name { get; set; }
        public IEnumerable<ProjectVersionTreeNode> ProjectVersions { get; set; }

        public override string ToString()
        {
            return $"Id = {ProjectVersionId}, Name = {Name}";
        }
    }
}