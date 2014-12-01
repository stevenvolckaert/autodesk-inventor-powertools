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

        #region Loading Data

        /// <summary>
        /// Occurs when the view model starts loading data.
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

        private bool _isDataLoading;
        /// <summary>
        /// Gets a value that indicates whether the view model's data is loading.
        /// </summary>
        public virtual bool IsDataLoading
        {
            get { return _isDataLoading; }
            protected set
            {
                if (_isDataLoading != value)
                {
                    _isDataLoading = value;
                    OnPropertyChanged(() => IsBusy);
                    OnPropertyChanged(() => IsDataLoading);
                }
            }
        }

        /// <summary>
        /// Occurs when the view model's data is loaded.
        /// </summary>
        public event EventHandler DataLoaded;

        /// <summary>
        /// Raises the <see cref="DataLoaded"/> event,
        /// and sets the <see cref="IsDataLoading"/> and <see cref="IsDataLoaded"/> properties accordingly.
        /// </summary>
        private void OnDataLoaded()
        {
            IsDataLoading = false;
            IsDataLoaded = true;

            if (DataLoaded != null)
                DataLoaded(this, EventArgs.Empty);
        }

        private bool _isDataLoaded;
        /// <summary>
        /// Gets a value that indicates whether the view model's data is loaded.
        /// </summary>
        public bool IsDataLoaded
        {
            get { return _isDataLoaded; }
            set
            {
                if (_isDataLoaded != value)
                {
                    _isDataLoaded = value;
                    OnPropertyChanged(() => IsDataLoaded);
                }
            }
        }

        /// <summary>
        /// Gets or sets the method that loads the view model's data from the application's back end.
        /// </summary>
        protected Task LoadDataTask { get; set; }

        /// <summary>
        /// Gets a command that, when executed, loads the view model's data from the application's back end.
        /// </summary>
        public DelegateCommand LoadDataCommand { get; private set; }

        /// <summary>
        /// Loads the view model's data from the application's back end.
        /// </summary>
        public async void LoadData()
        {
            if (LoadDataTask == null)
                return;

            OnDataLoading();
            await LoadDataTask;
            OnDataLoaded();
        }

        /// <summary>
        /// Returns a value that indicates whether <see cref="LoadDataCommand"/> can be executed.
        /// </summary>
        protected virtual bool CanLoadDataCommandExecute()
        {
            return true;
        }

        #endregion

        #region Saving Data

        /// <summary>
        /// Occurs when the view model starts saving data.
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

        private bool _isDataSaving;
        /// <summary>
        /// Gets a value that indicates whether the view model's data is saving.
        /// </summary>
        public virtual bool IsDataSaving
        {
            get { return _isDataSaving; }
            protected set
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
        /// Gets or sets the method that loads the view model's data from the application's back end.
        /// </summary>
        protected Task SaveDataTask { get; set; }

        public DelegateCommand SaveDataCommand { get; private set; }

        /// <summary>
        /// Saves the view model's data to the application's back end.
        /// </summary>
        public async void SaveData()
        {
            if (SaveDataTask == null)
                return;

            OnDataSaving();
            await SaveDataTask;
            OnDataSaved();
        }

        /// <summary>
        /// Returns a value that indicates whether <see cref="SaveDataCommand"/> can be executed.
        /// </summary>
        protected virtual bool CanSaveDataCommandExecute()
        {
            return true;
        }

        #endregion

        public DelegateCommand RefreshDataCommand { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StevenVolckaert.Windows.Mvvm.DataViewModelBase"/> class.
        /// </summary>
        protected DataViewModelBase()
        {
            LoadDataCommand = new DelegateCommand(LoadData, CanLoadDataCommandExecute);
            SaveDataCommand = new DelegateCommand(SaveData, CanSaveDataCommandExecute);
            RefreshDataCommand = new DelegateCommand(RefreshData, CanRefreshDataCommandExecute);
        }

        /// <summary>
        /// Returns a value that indicates whether <see cref="RefreshDataCommand"/> can be executed.
        /// </summary>
        protected virtual bool CanRefreshDataCommandExecute()
        {
            return true;
        }

        /// <summary>
        /// Refreshes (reloads) the view model's data.
        /// </summary>
        public virtual void RefreshData()
        {
            LoadData();
        }
    }
}
