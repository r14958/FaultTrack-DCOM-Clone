// -----------------------------------------------------------------------------
//  <copyright file="FocusRequestedEventArgs.cs" company="DCOM Engineering, LLC">
//      Copyright (c) DCOM Engineering, LLC.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------
namespace FaultTrack.Windows
{
    using System;

    public class FocusRequestedEventArgs : EventArgs
    {
        public FocusRequestedEventArgs(string propertyName)
        {
            PropertyName = propertyName;
        }

        public string PropertyName
        {
            get;
            private set;
        }
    }
}