namespace FaultTrack.Shell
{
    public interface IProject
    {
        int Id { get; }
        bool IsExpanded { get; set; }
        bool IsSelected { get; set; }
        string Name { get; }
        IProjectCollection ProjectCollection { get; }
        IProject ParentProject { get; }
        IProjects Projects { get; }
        IProjectVersions ProjectVersions { get; }
        void ChangeParent(IProject project);
        void ChangeParent(IProjectCollection collection);
    }
}