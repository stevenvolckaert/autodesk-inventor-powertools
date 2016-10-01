namespace StevenVolckaert.InventorPowerTools
{
    using Inventor;
    using Microsoft.Practices.Prism.ViewModel;

    public abstract class ModelBase : NotificationObject
    {
        protected static _Document ActiveDocument
        {
            get { return AddIn.Inventor.ActiveDocument; }
        }

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

        /// <summary>
        /// Returns the active drawing document, or <c>null</c> if the active document is not a
        /// drawing document, or no document is active.
        /// </summary>
        /// <returns>The active <see cref="DrawingDocument"/> instance, or <c>null</c> if there is none.</returns>
        protected DrawingDocument TryGetActiveDrawingDocument()
        {
            return ActiveDocument as DrawingDocument;
        }
    }
}
