namespace FaultTrack.Windows.SolutionExplorer.Views
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    /// <summary>
    /// View that represents FaultTrack.Windows.SolutionExplorer.SolutionExplorerViewModel.
    /// </summary>
    public partial class SolutionExplorerView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the FaultTrack.Windows.SolutionExplorer.SolutionExplorerView class.
        /// </summary>
        public SolutionExplorerView()
        {
            InitializeComponent();
        }

        private void TreeViewItem_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ClickCount > 1 && TreeView.SelectedItem is Solution)
            {
                Solution solution = (Solution)TreeView.SelectedItem;

                Process.Start(solution.FilePath);
            }
            
            e.Handled = true;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var stack = new Stack<ITreeViewItem>(TreeView.ItemsSource.Cast<ITreeViewItem>());

            while (stack.Count > 0)
            {
                ITreeViewItem item = stack.Pop();

                foreach (ITreeViewItem child in item.Items)
                {
                    stack.Push(child);
                }
                
                ICollectionView view = CollectionViewSource.GetDefaultView(item.Items);

                view.Filter = p =>
                {
                    if (String.IsNullOrWhiteSpace(SearchTextBox.Text))
                    {
                        return true;
                    }
                    
                    Solution solution = (Solution)p;

                    return solution.Name.ToLower().Contains(SearchTextBox.Text.ToLower());
                };

                view.Refresh();
            }

            ICollectionView rootView = CollectionViewSource.GetDefaultView(TreeView.Items);

            rootView.Filter = p =>
            {
                Project project = (Project)p;

                ICollectionView subview = CollectionViewSource.GetDefaultView(project.Items);

                if (subview.IsEmpty)
                {
                    return false;
                }
                else
                {
                    project.IsExpanded = true;
                }

                if (String.IsNullOrWhiteSpace(SearchTextBox.Text))
                {
                    project.IsExpanded = false;
                }

                return true;
            };
            
            rootView.Refresh();
        }
    }
}