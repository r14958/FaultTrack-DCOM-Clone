namespace Bootstrapper
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Net;
    using System.Reflection;
    using System.Web.Script.Serialization;
    using System.Windows.Input;

    public class GetServerVersionCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public Version Version
        {
            get;
            private set;
        }

        public void Execute(object parameter)
        {
            using (var client = new WebClient())
            {
                var data = client.DownloadString("http://localhost:51744/api/bootstrapper/getserverversion");
                var json = new JavaScriptSerializer().Deserialize<GetServerVersionResponse>(data);

                Version = new Version(json.Version);
            }   
        }

        class GetServerVersionResponse
        {
            public string Version { get; set; }
        }
    }

    public class DownloadCommand : ICommand, INotifyPropertyChanged
    {
        private int progress;
        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler Completed;

        public bool IsBusy
        {
            get;
            private set;
        }

        public int Progress
        {
            get
            {
                return progress;
            }
            set
            {
                progress = value;
                OnPropertyChanged(nameof(Progress));
            }
        }

        public bool CanExecute(object parameter)
        {
            return !IsBusy;
        }

        public void Execute(object parameter)
        {
            using (var client = new WebClient())
            {
                client.DownloadProgressChanged += OnDownloadProgressChanged;
                client.DownloadFileCompleted += OnDownloadCompleted;
                client.DownloadFileAsync(new Uri("http://localhost:51744/api/bootstrapper/downloadclient"), @"F:\DCOM\FaultTrack\Main\Build\Client\FaultTrack.new.exe");
            }
        }

        private void OnDownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            IsBusy = false;
            Completed?.Invoke(this, EventArgs.Empty);
        }

        private void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Progress = e.ProgressPercentage;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class MainWindowViewModel
    {
        public event EventHandler RequestClose;

        public MainWindowViewModel()
        {
            GetServerVersionCommand = new GetServerVersionCommand();
            DownloadCommand = new DownloadCommand();
        }

        public GetServerVersionCommand GetServerVersionCommand
        {
            get;
            private set;
        }

        public DownloadCommand DownloadCommand
        {
            get;
            private set;
        }

        public void Load()
        {
            GetServerVersionCommand.Execute(null);

            var version = Assembly.GetExecutingAssembly().GetName().Version;

            if (GetServerVersionCommand.Version > version)
            {

                DownloadCommand.Completed += DownloadCommand_Completed;
                DownloadCommand.Execute(null);
            }
            else
            {
                RequestClose?.Invoke(this, EventArgs.Empty);
            }
        }

        private void DownloadCommand_Completed(object sender, System.EventArgs e)
        {
            Process.Start(@"F:\DCOM\FaultTrack\Main\Build\Client\FaultTrack.new.exe");
            RequestClose?.Invoke(this, EventArgs.Empty);
        }
    }
}