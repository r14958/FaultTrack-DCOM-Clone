namespace FaultTrack.Shell
{
    using System.Collections.Generic;

    public interface IProjects : IEnumerable<IProject>
    {
        void Add(IProject item);
        bool Remove(IProject item);
        void Clear();
    }
}