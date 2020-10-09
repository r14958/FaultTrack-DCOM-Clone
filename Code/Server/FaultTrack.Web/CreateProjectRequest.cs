namespace FaultTrack.Web
{
    public class CreateProjectRequest
    {
        public int ProjectCollectionId { get; set; }
        public int? ParentProjectId { get; set; }
        public string Name { get; set; }
    }
}