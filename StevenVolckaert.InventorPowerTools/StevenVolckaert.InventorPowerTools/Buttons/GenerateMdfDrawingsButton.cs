using System;
using Inventor;
using StevenVolckaert.InventorPowerTools.Windows;

namespace StevenVolckaert.InventorPowerTools.Buttons
{
    internal class GenerateMdfDrawingsButton : ButtonBase
    {
        private readonly GenerateMdfDrawingsWindow _generateDrawingsWindow = new GenerateMdfDrawingsWindow();

        public override string DisplayName
        {
            get { return "MDF"; }
        }

        public override string Description
        {
            get { return "Generate a drawing of every\r\nMDF part in the active document."; }
        }

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
                    ShowWarningMessageBox(
                        messageFormat: "Assembly {0} doesn't contain any MDF parts.\r\n\r\n" +
                                       "The file name of MDF parts must contain 'MDF' to be recognised as an MDF part.",
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
