namespace FaultTrack.Data
{
    public class ProjectTreeNode
    {
        public ProjectTreeNodeType Type { get; set; }
        public string Name { get; set; }
        public int ProjectCollectionId { get; set; }
        public int? ProjectId { get; set; }
        public int? ProjectVersionId { get; set; }
        public int? ParentProjectId { get; set; }
        public int? ParentProjectVersionId { get; set; }
    }
}