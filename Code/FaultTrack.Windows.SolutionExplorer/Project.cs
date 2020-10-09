namespace FaultTrack.Windows.SolutionExplorer
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class Project : ObservableObject, ITreeViewItem
    {
        private ICollection<ITreeViewItem> items = new ObservableCollection<ITreeViewItem>();
        private bool isSelected;
        private bool isExpanded;
        private bool initialized;

        public IEnumerable<Solution> Branches { get; set; }
        public Solution Baseline { get; set; }
        public string Name { get; set; }

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

        public ICollection<ITreeViewItem> Items
        {
            get
            {
                if (!initialized)
                {
                    if (Baseline == null)
                        foreach (var item in Branches.Cast<ITreeViewItem>()) 
                        {
                            items.Add(item);
                        }
                    else
                        foreach (var item in  new ITreeViewItem[] { Baseline }.Concat(Branches))
                        {
                            items.Add(item);
                        }
                    initialized = true;
                }

                return items;
            }
        }
    }
}
