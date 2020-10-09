namespace FaultTrack.Windows.Views
{
    using System.Windows;
    using System.Windows.Controls;

    public partial class ConnectView : UserControl
    {
        public ConnectView()
        {
            InitializeComponent();
        }

        private void ConnectView_OnLoaded(object sender, RoutedEventArgs e)
        {
            AccountTextBox.Focus();
            AccountTextBox.SelectAll();
        }
    }
}