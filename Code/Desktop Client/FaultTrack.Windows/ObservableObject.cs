// -----------------------------------------------------------------------------
//  <copyright file="ObservableObject.cs" company="DCOM Engineering, LLC">
//      Copyright (c) DCOM Engineering, LLC.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------
namespace FaultTrack.Windows
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// A ViewModel class created by danderson on 4/10/2013 4:01:55 PM.
    /// </summary>
    public class ObservableObject : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        /// <summary>
        /// Occurs when a property that supports change notification has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event for a property.
        /// </summary>
        /// <param name="propertyName">Optional. The name of the property that has changed.</param>
        /// <remarks>
        ///   If the name of the property is not provided it is inferred by the caller. 
        ///   The inferred name can be incorrect if the caller is not actually the changed property.
        /// </remarks>
        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}