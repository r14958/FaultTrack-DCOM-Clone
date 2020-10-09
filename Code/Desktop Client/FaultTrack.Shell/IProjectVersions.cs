namespace FaultTrack.Shell
{
    using System.Collections.Generic;

    public interface IProjectVersions : IEnumerable<IProjectVersion>
    {
        void Add(IProjectVersion item);
        void Clear();
    }
}