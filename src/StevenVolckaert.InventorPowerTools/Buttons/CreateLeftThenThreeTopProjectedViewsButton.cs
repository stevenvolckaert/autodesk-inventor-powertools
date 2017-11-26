namespace StevenVolckaert.InventorPowerTools.Buttons
{
    using System;
    using System.Windows;
    using Inventor;
    using Environment = System.Environment;

    internal class CreateLeftThenThreeTopProjectedViewsButton : ButtonBase
    {
        public override string DisplayName { get; } = "Left+" + Environment.NewLine + "3×Top";

        public override string Description { get; } =
            "Add a Left Projected View," + Environment.NewLine + "then three Top Projected Views";

        public CreateLeftThenThreeTopProjectedViewsButton()
        {
            Panel = AddIn.UserInterfaceManager.GetRibbonPanel(
                name: "id_PanelD_PlaceViewsCreate",
                tabName: "id_TabPlaceViews",
                ribbonName: "Drawing"
            );
        }

        protected override void OnExecute(NameValueMap context)
        {
            var transaction = AddIn.CreateTransaction(DisplayName);

            try
            {
                foreach (var drawingView in AddIn.GetDrawingViews("Select a view"))
                    drawingView.AddLeftThenTopProjectedViews(
                        numberOfTopViews: 3,
                        addDimensions: true,
                        drawingDistance: 2.5
                    );

                transaction.End();
            }
            catch (Exception ex)
            {
                transaction.Abort();
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
