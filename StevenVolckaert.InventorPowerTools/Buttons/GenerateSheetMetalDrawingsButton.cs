namespace StevenVolckaert.InventorPowerTools.Buttons
{
    using System;
    using Inventor;
    using Windows;
    using Environment = System.Environment;

    internal class GenerateSheetMetalDrawingsButton : ButtonBase
    {
        private readonly GenerateSheetMetalDrawingsWindow _generateSheetMetalDrawingsWindow =
            new GenerateSheetMetalDrawingsWindow();

        public override string DisplayName { get; } = "Sheet Metal";

        public override string Description { get; } =
            "Generate a drawing of every sheet" + Environment.NewLine +
            "metal part in the active document.";

        protected override void OnExecute(NameValueMap context)
        {
            try
            {
                var assembly = AddIn.GetAssemblyDocument();

                if (assembly == null)
                    return;

                var sheetMetalParts = assembly.SheetMetalParts();

                if (sheetMetalParts.Count == 0)
                {
                    ShowWarningMessageBox(
                        $"Assembly {assembly.FullFileName} doesn't contain any sheet metal parts."
                    );
                    return;
                }

                _generateSheetMetalDrawingsWindow.Show(assembly, sheetMetalParts);
            }
            catch (Exception ex)
            {
                ShowWarningMessageBox(ex);
            }
        }
    }
}
