namespace FaultTrack.Shell.Infrastructure
{
    using System.Collections.ObjectModel;

    internal sealed class ProjectCollections : ObservableCollection<IProjectCollection>, IProjectCollections
    {
    }
}