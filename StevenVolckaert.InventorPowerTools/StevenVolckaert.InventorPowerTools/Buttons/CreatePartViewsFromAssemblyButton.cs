namespace StevenVolckaert.InventorPowerTools.Buttons
{
    using System;
    using System.Windows;
    using Inventor;

    internal class CreatePartViewsFromAssemblyButton : ButtonBase
    {
        public override string DisplayName { get; } = "Place Parts";

        public override string Description { get; } = "Create base views for every part in an assembly";

        public CreatePartViewsFromAssemblyButton()
        {
            Panel = AddIn.UserInterfaceManager.GetPanel(
                name: "id_PanelD_PlaceViewsCreate",
                tabName: "id_TabPlaceViews",
                ribbonName: "Drawing"
            );
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
                        MessageBox.Show(
                            messageBoxText: DisplayName + " can only be used on base views.",
                            caption: DisplayName,
                            button: MessageBoxButton.OK,
                            icon: MessageBoxImage.Information
                        );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
