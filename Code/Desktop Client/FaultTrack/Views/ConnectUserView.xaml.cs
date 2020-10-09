namespace FaultTrack.Windows.Views
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using ViewModels;
    
    public partial class ConnectUserView : UserControl
    {
        public ConnectUserView()
        {
            InitializeComponent();
        }

        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            var viewModel = (PrincipalViewModel)DataContext;

            viewModel.Password = ((PasswordBox)sender).Password;
        }

        private void ConnectUserView_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(UserNameTextBox.Text))
            {
                UserNameTextBox.Focus();
                UserNameTextBox.SelectAll();
            }
            else
            {
                PasswordTextBox.Focus();
                PasswordTextBox.SelectAll();
            }

            var viewModel = (PrincipalViewModel)DataContext;

            if (viewModel.CanAutoConnect)
            {
                viewModel.SignInCommand.Execute(null);
            }
        }

        private void ConnectUserView_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(e.NewValue is PrincipalViewModel))
            {
                return;
            }

            var viewModel = (PrincipalViewModel)e.NewValue;

            viewModel.FocusRequested += ViewModelOnFocusRequested;
        }

        private void ViewModelOnFocusRequested(object sender, FocusRequestedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Password":
                    PasswordTextBox.Focus();
                    PasswordTextBox.SelectAll();
                    break;
            }
        }
    }
}