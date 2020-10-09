namespace FaultTrack.Shell
{
    using System.Collections.Generic;

    public interface IProjectCollections : IEnumerable<IProjectCollection>
    {
        void Add(IProjectCollection item);
        void Clear();
        int Count { get; }
    }
}