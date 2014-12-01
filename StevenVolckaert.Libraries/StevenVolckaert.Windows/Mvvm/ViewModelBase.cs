using System;

namespace StevenVolckaert.Windows.Mvvm
{
    /// <summary>
    /// Base class for MVVM view models, providing logic and data storage for MVVM views.
    /// </summary>
    [CLSCompliant(false)]
    public abstract class ViewModelBase : ModelBase
    {
        private string _title;
        /// <summary>
        /// Gets or sets the view model's title.
        /// </summary>
        [NotifyPropertyChangedOnCultureChange]
        public virtual string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged(() => Title);
                }
            }
        }

        private string _status;
        /// <summary>
        /// Gets the view model's current status, intended for display in MVVM views.
        /// </summary>
        [NotifyPropertyChangedOnCultureChange]
        public virtual string Status
        {
            get { return _status; }
            protected set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged(() => Status);
                }
            }
        }

        /// <summary>
        /// When overridden in a derived class, is invoked when all imports
        /// have been satisfied and are safe to use.
        /// </summary>
        public override void OnImportsSatisfied()
        {
            base.OnImportsSatisfied();
            RegisterCommands();
        }

        /// <summary>
        /// Registers the view model's commands.
        /// <para>The base implementation does nothing.</para>
        /// </summary>
        protected virtual void RegisterCommands()
        {
        }
    }
}
