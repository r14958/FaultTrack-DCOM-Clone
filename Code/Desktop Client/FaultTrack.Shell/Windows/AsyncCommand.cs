// -----------------------------------------------------------------------------
//  <copyright file="IAsyncCommand.cs" company="DCOM Engineering, LLC">
//      Copyright (c) DCOM Engineering, LLC.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace FaultTrack.Shell.Windows
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <inheritdoc cref="IAsyncCommand" />
    public abstract class AsyncCommand : IAsyncCommand
    {
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
        async void ICommand.Execute(object parameter)
        {
            Task task = ExecuteAsync();

            ConfiguredTaskAwaitable configuredTask = task.ConfigureAwait(false);

            await configuredTask;
        }

        /// <inheritdoc />
        public abstract bool CanExecute();

        /// <inheritdoc />
        public abstract Task ExecuteAsync();
    }
}