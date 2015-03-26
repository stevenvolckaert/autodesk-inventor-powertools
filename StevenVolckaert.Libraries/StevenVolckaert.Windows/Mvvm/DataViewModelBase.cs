using System;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Commands;

namespace StevenVolckaert.Windows.Mvvm
{
    /// <summary>
    /// Base class for MVVM view models that provides logic for loading and saving data.
    /// </summary>
    [CLSCompliant(false)]
    public abstract class DataViewModelBase : ViewModelBase
    {
        private class DataProvider : Data.DataProviderBase
        {
            private DataViewModelBase _dataViewModel;

            public DataProvider(DataViewModelBase dataViewModel)
            {
                _dataViewModel = dataViewModel;
            }

            protected override Task DoLoadData()
            {
                return _dataViewModel.DoLoadData();
            }
        }

        private readonly DataProvider _dataProvider;

        /// <summary>
        /// Gets the view model's current status, intended for display in MVVM views.
        /// </summary>
        public override string Status
        {
            get
            {
                if (base.Status != null)
                    return base.Status;

                if (IsDataLoading)
                    return Properties.Resources.Loading;

                if (IsDataSaving)
                    return Properties.Resources.Saving;

                return Properties.Resources.Idle;
            }
            protected set { base.Status = value; }
        }

        /// <summary>
        /// Gets a value that indicates whether the view model is busy,
        /// i.e. whether data is loading or saving.
        /// </summary>
        public bool IsBusy
        {
            get { return IsDataLoading || IsDataSaving; }
        }

        #region Members concerned with loading data

        /// <summary>
        /// Gets a value that indicates whether the view model's data is loading.
        /// </summary>
        public virtual bool IsDataLoading
        {
            get { return _dataProvider.IsDataLoading; }
        }

        /// <summary>
        /// Occurs when the view model starts loading data.
        /// </summary>
        public event EventHandler DataLoading;

        /// <summary>
        /// Raises the <see cref="DataLoading"/> event,
        /// and the <see cref="PropertyChanged"/> event for the <see cref="IsBusy"/> and <see cref="IsDataLoading"/> properties.
        /// </summary>
        protected void OnDataLoading()
        {
            OnPropertyChanged(() => IsBusy);
            OnPropertyChanged(() => IsDataLoading);
            OnPropertyChanged(() => IsDataLoaded);

            if (DataLoading != null)
                DataLoading(this, EventArgs.Empty);
        }

        /// <summary>
        /// Occurs when the view model's data is loaded.
        /// </summary>
        public event EventHandler DataLoaded;

        /// <summary>
        /// Raises the <see cref="DataLoaded"/> event,
        /// and the <see cref="PropertyChanged"/> event for the <see cref="IsBusy"/> and <see cref="IsDataLoading"/> properties.
        /// </summary>
        private void OnDataLoaded()
        {
            OnPropertyChanged(() => IsBusy);
            OnPropertyChanged(() => IsDataLoading);
            OnPropertyChanged(() => IsDataLoaded);

            if (DataLoaded != null)
                DataLoaded(this, EventArgs.Empty);
        }

        /// <summary>
        /// Gets a value that indicates whether the view model's data is loaded.
        /// </summary>
        public bool IsDataLoaded
        {
            get { return _dataProvider.IsDataLoaded; }
        }

        /// <summary>
        /// Gets a command that, when executed, loads the view model's data from the application's back end.
        /// </summary>
        public DelegateCommand LoadDataCommand { get; private set; }

        /// <summary>
        /// Returns a value that indicates whether <see cref="LoadDataCommand"/> can be executed.
        /// </summary>
        protected virtual bool CanLoadDataCommandExecute()
        {
            return true;
        }

        /// <summary>
        /// Loads the view model's data from the application's back end asynchonously.
        /// </summary>
        /// <exception cref="NotImplementedException">The method is not implemented.</exception>
        public async Task LoadDataAsync()
        {
            await _dataProvider.LoadDataAsync();
        }

        /// <summary>
        /// Provides the logic required to load data from the application's back end.
        /// </summary>
        /// <exception cref="NotImplementedException">The method is not implemented.</exception>
        protected virtual Task DoLoadData()
        {
            throw new NotImplementedException();
        }

        public DelegateCommand ReloadDataCommand { get; private set; }

        /// <summary>
        /// Returns a value that indicates whether <see cref="ReloadDataCommand"/> can be executed.
        /// </summary>
        protected virtual bool CanReloadDataCommandExecute()
        {
            return true;
        }

        /// <summary>
        /// Reloads the view model's data asynchronously.
        /// </summary>
        /// <exception cref="NotImplementedException">The method is not implemented.</exception>
        public Task ReloadDataAsync()
        {
            return _dataProvider.ReloadDataAsync();
        }

        #endregion

        #region Members concerned with saving data

        private bool _isDataSaving;
        /// <summary>
        /// Gets a value that indicates whether the view model's data is saving.
        /// </summary>
        public virtual bool IsDataSaving
        {
            get { return _isDataSaving; }
            private set
            {
                if (_isDataSaving != value)
                {
                    _isDataSaving = value;
                    OnPropertyChanged(() => IsBusy);
                    OnPropertyChanged(() => IsDataSaving);
                }
            }
        }

        /// <summary>
        /// Occurs when the view model's data is saving.
        /// </summary>
        public event EventHandler DataSaving;

        /// <summary>
        /// Raises the <see cref="DataSaving"/> event,
        /// and sets the <see cref="IsDataSaving"/> and <see cref="IsDataSaved"/> properties accordingly.
        /// </summary>
        protected void OnDataSaving()
        {
            IsDataSaving = true;
            IsDataSaved = false;

            if (DataSaving != null)
                DataSaving(this, EventArgs.Empty);
        }

        private bool _isDataSaved;
        /// <summary>
        /// Gets a value that indicates whether the view model's data is saved.
        /// </summary>
        public bool IsDataSaved
        {
            get { return _isDataSaved; }
            set
            {
                if (_isDataSaved != value)
                {
                    _isDataSaved = value;
                    OnPropertyChanged(() => IsDataSaved);
                }
            }
        }

        /// <summary>
        /// Occurs when the view model's data is saved.
        /// </summary>
        public event EventHandler DataSaved;

        /// <summary>
        /// Raises the <see cref="DataSaved"/> event,
        /// and sets the <see cref="IsDataSaving"/> and <see cref="IsDataSaved"/> properties accordingly.
        /// </summary>
        private void OnDataSaved()
        {
            IsDataSaving = false;
            IsDataSaved = true;

            if (DataSaved != null)
                DataSaved(this, EventArgs.Empty);
        }

        /// <summary>
        /// Gets a command that, when executed, saves the view model's data to the application's back end.
        /// </summary>
        public DelegateCommand SaveDataCommand { get; private set; }

        /// <summary>
        /// Returns a value that indicates whether <see cref="SaveDataCommand"/> can be executed.
        /// </summary>
        protected virtual bool CanSaveDataCommandExecute()
        {
            return true;
        }

        /// <summary>
        /// Saves the view model's data to the application's back end asynchronously.
        /// </summary>
        /// <param name="ignoreState">
        /// A value that indicates whether to ignore the data's current state.
        /// <para>When <c>true</c>, data is saved even if no data changes have been detected.</para>
        /// </param>
        /// <exception cref="NotImplementedException">The method is not implemented.</exception>
        public async Task SaveDataAsync(bool ignoreState)
        {
            if (ignoreState)
                IsDataSaved = false;

            await SaveDataAsync();
        }

        /// <summary>
        /// Saves the view model's data to the application's back end asynchronously.
        /// </summary>
        /// <exception cref="NotImplementedException">The method is not implemented.</exception>
        public async Task SaveDataAsync()
        {
            if (IsDataSaved)
                return;

            OnDataSaving();
            await DoSaveData();
            OnDataSaved();
        }

        /// <summary>
        /// Provides the logic required to save data to the application's back end.
        /// </summary>
        protected virtual Task DoSaveData()
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="StevenVolckaert.Windows.Mvvm.DataViewModelBase"/> class.
        /// </summary>
        protected DataViewModelBase()
        {
            _dataProvider = new DataProvider(this);
            _dataProvider.DataLoading += (sender, e) => OnDataLoading();
            _dataProvider.DataLoaded += (sender, e) => OnDataLoaded();

            LoadDataCommand = DelegateCommand.FromAsyncHandler(LoadDataAsync, CanLoadDataCommandExecute);
            SaveDataCommand = DelegateCommand.FromAsyncHandler(SaveDataAsync, CanSaveDataCommandExecute);
            ReloadDataCommand = DelegateCommand.FromAsyncHandler(ReloadDataAsync, CanReloadDataCommandExecute);
        }
    }
}
