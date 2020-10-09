namespace FaultTrack.Shell
{
    using System.Collections.Generic;
    using System.Collections.Specialized;

    public interface ITabs : IEnumerable<ITab>, INotifyCollectionChanged
    {
        void Add(ITab tab);
        bool Remove(ITab tab);
    }
}