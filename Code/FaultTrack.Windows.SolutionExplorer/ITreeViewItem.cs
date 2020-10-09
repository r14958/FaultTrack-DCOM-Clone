namespace FaultTrack.Windows.SolutionExplorer
{
    using System.Collections.Generic;

    public interface ITreeViewItem
    {
        bool IsExpanded { get; set; }
        bool IsSelected { get; set; }
        ICollection<ITreeViewItem> Items { get; }
        string Name { get; }
    }
}