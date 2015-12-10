namespace StevenVolckaert.InventorPowerTools
{
    public abstract class ViewModelBase : Microsoft.Practices.Prism.ViewModel.NotificationObject
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    RaisePropertyChanged(() => Title);
                }
            }
        }

        protected ViewModelBase()
        {
            RegisterCommands();
        }

        protected virtual void RegisterCommands()
        {
        }

        protected void ShowWarningMessageBox(string messageFormat, params object[] messageArgs)
        {
            AddIn.ShowWarningMessageBox(Title, messageFormat, messageArgs);
        }
    }
}
