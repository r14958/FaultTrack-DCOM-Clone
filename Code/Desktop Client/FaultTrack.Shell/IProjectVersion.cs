namespace FaultTrack.Shell
{
    public interface IProjectVersion
    {
        int Id { get; }
        bool IsExpanded { get; set; }
        bool IsSelected { get; set; }
        string Name { get; }
        IProjectVersions ProjectVersions { get; }
    }
}