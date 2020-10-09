// -----------------------------------------------------------------------------
//  <copyright file="IRequestClose.cs" company="DCOM Engineering, LLC">
//      Copyright (c) DCOM Engineering, LLC.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------
namespace FaultTrack.Windows
{
    using System;

    /// <summary>
    /// Defines a contract for an object that may request that it can be closed.
    /// </summary>
    public interface IRequestClose
    {
        /// <summary>
        /// Occurs when the view model is requesting to be closed.
        /// </summary>
        event EventHandler CloseRequested;
    }
}