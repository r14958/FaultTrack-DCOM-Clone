namespace FaultTrack.Web
{
    using System.Collections.Generic;

    public class ProjectCollectionTreeNode
    {
        public int ProjectCollectionId { get; set; }
        public string Name { get; set; }
        public IEnumerable<ProjectTreeNode> Projects { get; set; }

        public override string ToString()
        {
            return $"Id = {ProjectCollectionId}, Name = {Name}";
        }
    }
}
