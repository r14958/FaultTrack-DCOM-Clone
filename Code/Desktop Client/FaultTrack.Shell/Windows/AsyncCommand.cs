// -----------------------------------------------------------------------------
//  <copyright file="IAsyncCommand.cs" company="DCOM Engineering, LLC">
//      Copyright (c) DCOM Engineering, LLC.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace FaultTrack.Shell.Windows
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <inheritdoc cref="IAsyncCommand" />
    public abstract class AsyncCommand : IAsyncCommand
    {
        private readonly ObservableCollection<Task> runningTasks;

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncCommand"/> class.
        /// </summary>
        protected AsyncCommand()
        {
            runningTasks = new ObservableCollection<Task>();
            runningTasks.CollectionChanged += OnRunningTasksChanged;
        }

        /// <inheritdoc />
        public IEnumerable<Task> RunningTasks
        {
            get => runningTasks;
        }

        /// <inheritdoc />
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <inheritdoc />
        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }

        /// <inheritdoc />
        public abstract bool CanExecute();

        /// <inheritdoc />
        async void ICommand.Execute(object parameter)
        {
            Task runningTask = ExecuteAsync();

            runningTasks.Add(runningTask);

            try
            {
                await runningTask;
            }
            finally
            {
                runningTasks.Remove(runningTask);
            }
        }

        /// <inheritdoc />
        public abstract Task ExecuteAsync();

        private void OnRunningTasksChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}