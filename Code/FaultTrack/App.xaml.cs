// -----------------------------------------------------------------------------
//  <copyright file="App.cs" company="DCOM Engineering, LLC">
//      Copyright (c) DCOM Engineering, LLC.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace FaultTrack.Windows
{
    using System.Windows;
    using Models;
    using ViewModels;
    using Views;

    /// <summary>
    /// Class that contains the main application logic like startup and configuration.
    /// </summary>
    public partial class App
    {
        /// <inheritdoc />
        protected override void OnStartup(StartupEventArgs e) 
        {
            base.OnStartup(e);

            var shell  = new Shell();
            var window = new ShellWindow
                         {
                             DataContext = new ShellViewModel(shell)
                         };

            window.ShowDialog();
        }
    }
}