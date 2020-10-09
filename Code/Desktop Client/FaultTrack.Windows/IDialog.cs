namespace FaultTrack.Windows
{
    using System.Windows;

    /// <summary>
    /// A MVVM dialog contract.
    /// </summary>
    public interface IDialog
    {
        /// <summary>
        /// Gets or sets the views data context.
        /// </summary>
        object DataContext
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the dialog result value, which is the value that is returned
        /// from the <see cref="ShowDialog"/> method.
        /// </summary>
        bool? DialogResult
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <see cref="Window"/> that owns this dialog.
        /// </summary>
        Window Owner
        {
            get;
            set;
        }

        /// <summary>
        /// Closes the dialog.
        /// </summary>
        void Close();

        /// <summary>
        /// Displays the dialog.
        /// </summary>
        /// <returns>Returns true if the user accepted the dialog, otherwise returns false. If
        /// the user neither accepted or cancelled the dialog, a null value is returned.</returns>
        bool? ShowDialog();
    }
}