namespace FaultTrack.Windows
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Base class for all view-model classes.
    /// </summary>
    public abstract class ViewModel : ObservableObject, INotifyDataErrorInfo
    {
        private event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        private readonly IDictionary<String, IEnumerable<String>> errors;
        private readonly object eventLock;

        protected ViewModel()
        {
            errors = new Dictionary<string, IEnumerable<string>>();
            eventLock = new object();
        }

        /// <summary>
        /// Gets a value indicating whether the object is valid.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return !((INotifyDataErrorInfo)this).HasErrors;
            }
        }

        /// <summary>
        /// Gets a enumerable collection of validation errors, if any.
        /// </summary>
        public IEnumerable<KeyValuePair<string, IEnumerable<string>>> GetValidationErrors()
        {
            return errors;
        }

        /// <summary>
        /// Validates the specified property using <see cref="System.ComponentModel.DataAnnotations"/>.
        /// </summary>
        /// <param name="propertyName">The property to validate.</param>
        protected void ValidateProperty([CallerMemberName] string propertyName = null)
        {
            if (errors.ContainsKey(propertyName))
            {
                errors.Remove(propertyName);
            }

            Type type = GetType();

            PropertyInfo property = type.GetProperty(propertyName);

            object value = property.GetValue(this);

            var context = new ValidationContext(this)
                          {
                              MemberName = propertyName
                          };

            var results = new Collection<ValidationResult>();

            bool isValid = Validator.TryValidateProperty(value, context, results);
            
            if (!isValid)
            {
                errors.Add(propertyName, results.Select(p => p.ErrorMessage));
            }

            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));

            NotifyPropertyChanged(nameof(IsValid));
        }

        #region INotifyDataErrorInfo Members

        bool INotifyDataErrorInfo.HasErrors
        {
            get
            {
                return errors.Any();
            }
        }

        IEnumerable INotifyDataErrorInfo.GetErrors(string propertyName)
        {
            if (String.IsNullOrEmpty(propertyName) || !errors.ContainsKey(propertyName))
            {
                return null;
            }

            return errors[propertyName]; ;
        }

        event EventHandler<DataErrorsChangedEventArgs> INotifyDataErrorInfo.ErrorsChanged
        {
            add
            {
                lock (eventLock)
                {
                    ErrorsChanged += value;
                }
            }
            remove
            {
                lock (eventLock)
                {
                    ErrorsChanged -= value;
                }
            }
        }

        #endregion
    }
}