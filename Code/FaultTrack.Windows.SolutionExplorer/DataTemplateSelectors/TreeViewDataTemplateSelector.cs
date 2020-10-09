namespace FaultTrack.Windows.SolutionExplorer.DataTemplateSelectors
{
    using System.Windows;
    using System.Windows.Controls;

    public class TreeViewDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var element = container as FrameworkElement;

            if (element == null || item == null)
            {
                return null;
            }

            if (item is Project)
            {
                return element.FindResource("ProjectTemplate") as DataTemplate;
            }

            if (item is Solution)
            {
                return element.FindResource("SolutionTemplate") as DataTemplate;
            }

            return null;
        }
    }
}