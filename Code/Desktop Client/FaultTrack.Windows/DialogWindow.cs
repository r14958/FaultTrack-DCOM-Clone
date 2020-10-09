namespace FaultTrack.Windows
{
    using System;
    using System.Windows;
    using System.Windows.Media.Animation;

    /// <summary>
    /// A specialized window to be used as a modal dialog.
    /// </summary>
    public class DialogWindow : ThemedWindow, IDialog
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DialogWindow"/> class.
        /// </summary>
        public DialogWindow()
        {
            Background = SystemColors.ControlBrush;
            Opacity = 0;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Loaded += OnLoaded;
        }

        private void AnimateWindow()
        {
            Storyboard storyboard  = new Storyboard
            {
                BeginTime = TimeSpan.FromSeconds(0),
                Duration = Duration.Forever
            };

            DoubleAnimation animation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(500)
            };

            Storyboard.SetTarget(animation, this);
            Storyboard.SetTargetProperty(animation, new PropertyPath(UIElement.OpacityProperty));

            storyboard.Children.Add(animation);

            storyboard.Begin();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            AnimateWindow();
        }
    }
}