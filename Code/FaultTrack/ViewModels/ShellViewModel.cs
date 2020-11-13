// -----------------------------------------------------------------------------
//  <copyright file="ShellViewModel.cs" company="DCOM Engineering, LLC">
//      Copyright (c) DCOM Engineering, LLC.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace FaultTrack.Windows.ViewModels
{
    using Shell;

    /// <summary>
    /// Class that models the shell view.
    /// </summary>
    public class ShellViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShellViewModel"/> class.
        /// </summary>
        /// <param name="shell">A <see cref="IShell"/> instance.</param>
        public ShellViewModel(IShell shell)
        {
            Shell = shell;
        }

        /// <summary>
        /// Gets the <see cref="IShell"/> model that is used in this view model.
        /// </summary>
        public IShell Shell
        {
            get;
        }
    }
}