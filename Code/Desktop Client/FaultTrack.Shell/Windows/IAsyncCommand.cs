// -----------------------------------------------------------------------------
//  <copyright file="IAsyncCommand.cs" company="DCOM Engineering, LLC">
//      Copyright (c) DCOM Engineering, LLC.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace FaultTrack.Shell.Windows
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    /// Defines a command that executes an asynchronous method.
    /// </summary>
    public interface IAsyncCommand : ICommand
    {
        /// <summary>
        /// Gets a value indicating whether a task executed by this command is still running.
        /// </summary>
        IEnumerable<Task> RunningTasks
        {
            get;
        }

        /// <inheritdoc cref="ICommand.CanExecute" />
        bool CanExecute();

        /// <summary>
        /// Defines an asynchronous method to be called when the command is invoked. 
        /// </summary>
        /// <returns>Returns an asynchronous operation.</returns>
        Task ExecuteAsync();
    }
}