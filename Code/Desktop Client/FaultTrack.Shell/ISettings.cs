namespace FaultTrack.Shell
{
    public interface ISettings
    {
        string Account { get; }
        string UserName { get; }
        string UserToken { get; }
    }
}