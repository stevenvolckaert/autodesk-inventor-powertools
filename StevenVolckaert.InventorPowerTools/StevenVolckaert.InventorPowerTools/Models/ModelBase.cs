namespace StevenVolckaert.InventorPowerTools
{
    using Microsoft.Practices.Prism.ViewModel;

    public abstract class ModelBase : NotificationObject
    {
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    RaisePropertyChanged(() => IsSelected);
                }
            }
        }
    }
}
