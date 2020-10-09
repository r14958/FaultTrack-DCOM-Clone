namespace FaultTrack.Shell.Infrastructure
{
    using System.Collections.ObjectModel;

    internal sealed class Tabs : ObservableCollection<ITab>, ITabs
    {
    }
}