namespace FaultTrack.Web
{
    public class UpdateProjectRequest
    {
        public int ProjectId { get; set; }
        public int ProjectCollectionId { get; set; }
        public int? ParentProjectId { get; set; }
        public string Name { get; set; }
    }
}