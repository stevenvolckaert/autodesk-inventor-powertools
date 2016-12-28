namespace StevenVolckaert.InventorPowerTools.Buttons
{
    using System;
    using Inventor;
    using Windows;
    using Environment = System.Environment;

    internal class GenerateMdfDrawingsButton : ButtonBase
    {
        private readonly GenerateMdfDrawingsWindow _generateDrawingsWindow = new GenerateMdfDrawingsWindow();

        public override string DisplayName { get; } = "MDF";

        public override string Description { get; } =
            "Generate a drawing of every" + Environment.NewLine + "MDF part in the active document.";

        protected override void OnExecute(NameValueMap context)
        {
            try
            {
                var assemblyDocument = AddIn.GetAssemblyDocument();

                if (assemblyDocument == null)
                    return;

                var mdfParts = assemblyDocument.MdfParts();

                if (mdfParts.Count == 0)
                {
                    AddIn.ShowWarningMessageBox(
                        caption: _generateDrawingsWindow.Title,
                        messageFormat:
                            "Assembly '{0}' doesn't contain MDF parts."
                                + Environment.NewLine + Environment.NewLine
                                + "A part file name must contain 'MDF' to be recognized as a MDF part.",
                        messageArgs: assemblyDocument.FullFileName
                    );

                    return;
                }

                _generateDrawingsWindow.Show(assemblyDocument.MdfParts());
            }
            catch (Exception ex)
            {
                ShowWarningMessageBox(ex);
            }
        }
    }
}
