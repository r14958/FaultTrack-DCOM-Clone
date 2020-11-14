// -----------------------------------------------------------------------------
//  <copyright file="IAsyncCommand`.cs" company="DCOM Engineering, LLC">
//      Copyright (c) DCOM Engineering, LLC.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace FaultTrack.Shell.Windows
{
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    /// Defines a command that executes an asynchronous method.
    /// </summary>
    /// <typeparam name="T">The parameter type of the asynchronous method argument.</typeparam>
    public interface IAsyncCommand<in T> : ICommand
    {
        /// <inheritdoc cref="ICommand.CanExecute" />
        bool CanExecute(T parameter);

        /// <summary>
        /// Defines an asynchronous method to be called when the command is invoked. 
        /// </summary>
        /// <param name="parameter">A value that is passed to the method when the command is invoked.</param>
        /// <returns>Returns an asynchronous operation.</returns>
        Task ExecuteAsync(T parameter);
    }
}