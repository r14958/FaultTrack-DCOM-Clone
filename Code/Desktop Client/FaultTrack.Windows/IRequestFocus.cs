// -----------------------------------------------------------------------------
//  <copyright file="IRequestFocus.cs" company="DCOM Engineering, LLC">
//      Copyright (c) DCOM Engineering, LLC.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------
namespace FaultTrack.Windows
{
    using System;

    public interface IRequestFocus
    {
        event EventHandler<FocusRequestedEventArgs> FocusRequested;
    }
}