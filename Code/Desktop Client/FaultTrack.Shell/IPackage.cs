namespace FaultTrack.Shell
{
    using System.Threading.Tasks;

    public interface IPackage
    {
        IShell Shell { get; }
        Task LoadAsync();
    }
}