namespace FaultTrack.Shell
{
    public interface ITab
    {
        bool IsSelected { get; set; }
        string Name { get; set; }
        dynamic Template { get; }
    }
}