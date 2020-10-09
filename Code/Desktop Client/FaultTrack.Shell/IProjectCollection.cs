namespace FaultTrack.Shell
{
    public interface IProjectCollection
    {
        int Id { get; }
        bool IsExpanded { get; set; }
        bool IsSelected { get; set; }
        string Name { get; }
        IProjects Projects { get; }
    }
}