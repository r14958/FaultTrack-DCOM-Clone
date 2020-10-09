namespace FaultTrack.Windows
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    public interface IDialogService
    {
        /// <summary>
        /// Registers the specified <typeparamref name="TViewModel"/> to the specified <typeparamref name="TView"/>.
        /// </summary>
        /// <typeparam name="TViewModel">The <see cref="IDialogRequestClose"/> type to register.</typeparam>
        /// <typeparam name="TView">The <see cref="IDialog"/> type to register.</typeparam>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        void RegisterDialog<TViewModel, TView>() where TViewModel : IDialogRequestClose
                                                 where TView : IDialog;

        /// <summary>
        /// Displays the specified <paramref name="viewModel"/> instance.
        /// </summary>
        /// <typeparam name="TViewModel">The type that implements <see cref="IDialogRequestClose"/>.</typeparam>
        /// <param name="viewModel">The <see cref="IDialogRequestClose"/> instance.</param>
        /// <returns>Returns the dialog result.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        bool? ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : IDialogRequestClose;
    }
}