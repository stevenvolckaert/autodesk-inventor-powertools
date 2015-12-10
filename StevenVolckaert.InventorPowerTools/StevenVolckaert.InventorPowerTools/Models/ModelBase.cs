using Microsoft.Practices.Prism.ViewModel;
namespace StevenVolckaert.InventorPowerTools
{
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
