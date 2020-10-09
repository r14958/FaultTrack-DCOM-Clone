namespace FaultTrack.Windows
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    /// An implementation of System.Windows.Input.ICommand that takes an Func(Object, T) and Predicate(T).
    /// </summary>
    public class TaskCommand : ObservableObject, ICommand
    {
        private readonly Func<object, Task> action;
        private readonly Predicate<Object> predicate;
        private bool isExecuting;
        
        /// <summary>
        /// Initializes a new instance of the FaultTrack.RelayCommand class.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        public TaskCommand(Func<Object, Task> action) : this(action, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the FaultTrack.RelayCommand class.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        /// <param name="predicate">The predicate that determines if the action can be invoked.</param>
        public TaskCommand(Func<Object, Task> action, Predicate<Object> predicate)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action), "You must specify an Func<Object, T>.");
            }

            this.action = action;
            this.predicate = predicate;
        }

        /// <summary>
        /// Gets a value indicating whether the task is being awaited.
        /// </summary>
        public bool IsExecuting
        {
            get
            {
                return isExecuting;
            }
            private set
            {
                isExecuting = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Occurs when the <see cref="System.Windows.Input.CommandManager"/> detects conditions that might change the ability of a command to execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        /// <summary>
        /// Determines whether the command can execute.
        /// </summary>
        /// <param name="parameter">A custom parameter object.</param>
        /// <returns>
        ///     Returns true if the command can execute, otherwise returns false.
        /// </returns>
        public bool CanExecute(object parameter)
        {
            if (predicate == null)
            {
                return true;
            }

            return predicate(parameter);
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="parameter">A custom parameter object.</param>
        async void ICommand.Execute(object parameter)
        {
            IsExecuting = true;

            await ExecuteAsync(parameter);

            IsExecuting = false;
        }

        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <returns>Returns the promised <see cref="Task"/> object.</returns>
        public Task ExecuteAsync()
        {
            return ExecuteAsync(null);
        }

        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <param name="parameter">A custom parameter object.</param>
        /// <returns>Returns the promised <see cref="Task"/> object.</returns>
        public Task ExecuteAsync(object parameter)
        {
            return action(parameter);
        }
    }
}