// -----------------------------------------------------------------------------
//  <copyright file="AsyncCommand`.cs" company="DCOM Engineering, LLC">
//      Copyright (c) DCOM Engineering, LLC.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace FaultTrack.Shell.Windows
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <inheritdoc cref="IAsyncCommand{T}"/>
    public abstract class AsyncCommand<T> : IAsyncCommand<T>
    {
        /// <inheritdoc />
        public event EventHandler CanExecuteChanged
        {
            add    => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <inheritdoc />
        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute((T)parameter);
        }

        /// <inheritdoc />
        async void ICommand.Execute(object parameter)
        {
            Task task = ExecuteAsync((T)parameter);

            ConfiguredTaskAwaitable configuredTask = task.ConfigureAwait(false);

            await configuredTask;
        }

        /// <inheritdoc />
        public abstract bool CanExecute(T parameter);

        /// <inheritdoc />
        public abstract Task ExecuteAsync(T parameter);
    }
}