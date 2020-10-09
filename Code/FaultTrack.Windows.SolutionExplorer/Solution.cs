namespace FaultTrack.Windows.SolutionExplorer
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class Solution : ObservableObject, ITreeViewItem
    {
        private readonly ICollection<ITreeViewItem> items;
        private bool isSelected;
        private bool isExpanded;

        public Solution(string file)
        {
            items = new ObservableCollection<ITreeViewItem>();

            FilePath = file;
        }

        public string GroupName { get; set; }
        public string Name { get; set; }
        public string FilePath { get; }
        public ICollection<ITreeViewItem> Items => items;

        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                isExpanded = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                NotifyPropertyChanged();
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}