namespace FaultTrack.Windows.Services
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    /// <summary>
    /// The default dialog service.
    /// </summary>
    public sealed class DialogService : IDialogService
    {
        private readonly Window owner;

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogService"/> class.
        /// </summary>
        public DialogService(Window owner)
        {
            this.owner = owner;
            Mappings = new Dictionary<Type, Type>();
        }

        /// <summary>
        /// Gets the dialog mappings.
        /// </summary>
        public IDictionary<Type, Type> Mappings
        {
            get;
        } 
 
        /// <summary>
        /// Registers the specified <typeparamref name="TViewModel"/> to the specified <typeparamref name="TView"/>.
        /// </summary>
        /// <typeparam name="TViewModel">The <see cref="IDialogRequestClose"/> type to register.</typeparam>
        /// <typeparam name="TView">The <see cref="IDialog"/> type to register.</typeparam>
        public void RegisterDialog<TViewModel, TView>() where TViewModel : IDialogRequestClose 
                                                        where TView : IDialog
        {
            if (Mappings.ContainsKey(typeof(TViewModel)))
            {
                throw new ArgumentException(String.Format("Type {0} is already mapped to type {1}.",
                                                          typeof (TViewModel),
                                                          typeof (TView)));
            }

            Mappings.Add(typeof(TViewModel), typeof(TView));
        }

        /// <summary>
        /// Displays the specified <paramref name="viewModel"/> instance.
        /// </summary>
        /// <typeparam name="TViewModel">The type that implements <see cref="IDialogRequestClose"/>.</typeparam>
        /// <param name="viewModel">The <see cref="IDialogRequestClose"/> instance.</param>
        /// <param name="result">The <see cref="Action{T1, T2}"/> that is called when the dialog is closed.</param>
        public bool? ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : IDialogRequestClose
        {
            Type viewType = Mappings[typeof(TViewModel)];

            IDialog dialog = (IDialog)Activator.CreateInstance(viewType);

            EventHandler<DialogCloseRequestedEventArgs> handler = null;

            handler = (sender, e) =>
            {
                viewModel.CloseRequested -= handler;

                if (e.DialogResult.HasValue)
                {
                    dialog.DialogResult = e.DialogResult;
                }
                else
                {
                    dialog.Close();
                }
            };

            viewModel.CloseRequested += handler;

            dialog.DataContext = viewModel;
            dialog.Owner = owner;

            return dialog.ShowDialog();
        }
    }
}