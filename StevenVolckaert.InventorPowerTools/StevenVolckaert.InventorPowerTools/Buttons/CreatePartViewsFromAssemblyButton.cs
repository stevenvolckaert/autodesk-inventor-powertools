using System;
using System.Windows;
using Inventor;

namespace StevenVolckaert.InventorPowerTools.Buttons
{
    internal class CreatePartViewsFromAssemblyButton : ButtonBase
    {
        public override string DisplayName
        {
            get { return "Place Parts"; }
        }

        public override string Description
        {
            get { return "Create base views for every part in an assembly"; }
        }

        public CreatePartViewsFromAssemblyButton()
        {
            Panel = AddIn.UserInterfaceManager.GetPanel("id_PanelD_PlaceViewsCreate", tabName: "id_TabPlaceViews", ribbonName: "Drawing");
        }

        protected override void OnExecute(NameValueMap context)
        {
            try
            {
                var selectedView = AddIn.GetDrawingView("Select an assembly view");

                if (selectedView != null)
                    if (selectedView.ViewType == DrawingViewTypeEnum.kStandardDrawingViewType)
                    {
                        var transaction = AddIn.CreateTransaction(DisplayName);
                        selectedView.AddBaseViewOfParts(drawingDistance: 4);
                        transaction.End();
                    }
                    else
                        MessageBox.Show(DisplayName + " can only be used on base views.", DisplayName, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
