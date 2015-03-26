using System;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;

namespace StevenVolckaert.Windows.Data
{
    /// <summary>
    /// Base class for data providers.
    /// </summary>
    /// <typeparam name="T">The System.Type of the data provider's data.</typeparam>
    [CLSCompliant(false)]
    public abstract class DataProviderBase<T> : DataProviderBase
        where T : class
    {
        private T _data;
        /// <summary>
        /// Gets the data provider's data.
        /// </summary>
        public T Data
        {
            get { return _data; }
            protected set
            {
                if (_data != value)
                {
                    _data = value;
                    OnPropertyChanged(() => Data);
                }
            }
        }
    }

    /// <summary>
    /// Base class for data providers.
    /// </summary>
    [CLSCompliant(false)]
    public abstract class DataProviderBase : BindableBase
    {
        /// <summary>
        /// Gets a value that indicates whether the provider's data is loading.
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
        /// Gets a value that indicates whether the provider's data is loaded.
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
        /// Loads the provider's data from the application's back end asynchronously.
        /// </summary>
        public async Task LoadDataAsync()
        {
            if (IsDataLoaded)
                return;

            OnDataLoading();
            await DoLoadData();
            OnDataLoaded();
        }

        /// <summary>
        /// Provides the logic required to load data from the application's back end.
        /// </summary>
        protected abstract Task DoLoadData();

        /// <summary>
        /// Reloads the provider's data from the application's back end asynchronously.
        /// </summary>
        public async Task ReloadDataAsync()
        {
            IsDataLoaded = false;
            await LoadDataAsync();
        }
    }
}
