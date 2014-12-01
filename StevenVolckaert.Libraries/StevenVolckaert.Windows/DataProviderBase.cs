using System;
using Microsoft.Practices.Prism.Mvvm;

namespace StevenVolckaert.Windows
{
    /// <summary>
    /// Base class for data providers.
    /// </summary>
    [CLSCompliant(false)]
    public abstract class DataProviderBase : BindableBase
    {
        /// <summary>
        /// Gets a value that indicates whether the provider is loading data.
        /// </summary>
        public bool IsDataLoading { get; private set; }

        /// <summary>
        /// Occurs when the provider's data is loading.
        /// </summary>
        public event EventHandler DataLoading;

        /// <summary>
        /// Raises the <see cref="DataLoading"/> event,
        /// and sets the <see cref="IsDataLoading"/> and <see cref="IsDataLoaded"/> properties accordingly.
        /// </summary>
        protected void OnDataLoading()
        {
            IsDataLoading = true;
            IsDataLoaded = false;

            if (DataLoading != null)
                DataLoading(this, EventArgs.Empty);
        }

        /// <summary>
        /// Gets a value that indicates whether the provider's data is loaded
        /// and it is safe to use.
        /// </summary>
        public bool IsDataLoaded { get; private set; }

        /// <summary>
        /// Occurs when the provider's data is loaded.
        /// </summary>
        public event EventHandler DataLoaded;

        /// <summary>
        /// Raises the <see cref="DataLoaded"/> event,
        /// and sets the <see cref="IsDataLoading"/> and <see cref="IsDataLoaded"/> properties accordingly.
        /// </summary>
        protected void OnDataLoaded()
        {
            IsDataLoading = false;
            IsDataLoaded = true;

            if (DataLoaded != null)
                DataLoaded(this, EventArgs.Empty);
        }

        /// <summary>
        /// Loads the provider's data from the application's back end.
        /// </summary>
        public virtual void LoadData()
        {
            if (IsDataLoaded)
                return;

            ReloadData();
        }

        /// <summary>
        /// Reloads the provider's data from the application's back end.
        /// </summary>
        public abstract void ReloadData();
    }
}
