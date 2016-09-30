namespace StevenVolckaert.InventorPowerTools.Buttons
{
    using System;
    using Inventor;
    using StevenVolckaert.InventorPowerTools.Windows;
    using Environment = System.Environment;

    internal class GenerateSubassemblyDrawingsButton : ButtonBase
    {
        private readonly GenerateSubassemblyDrawingsWindow _generateDrawingsWindow = new GenerateSubassemblyDrawingsWindow();

        public override string DisplayName
        {
            get { return "Subassembly"; }
        }

        public override string Description
        {
            get { return "Generate a drawing of every" + Environment.NewLine + "subassembly in the active document."; }
        }

        protected override void OnExecute(NameValueMap context)
        {
            try
            {
                var assembly = AddIn.GetAssemblyDocument();

                if (assembly == null)
                    return;

                var subassemblies = assembly.Subassemblies();

                if (subassemblies.Count == 0)
                {
                    AddIn.ShowWarningMessageBox(
                        caption: _generateDrawingsWindow.Title,
                        messageFormat: "Assembly '{0}' doesn't contain subassemblies.",
                        messageArgs: assembly.FullFileName
                    );

                    return;
                }

                _generateDrawingsWindow.Show(subassemblies);
            }
            catch (Exception ex)
            {
                ShowWarningMessageBox(ex);
            }
        }
    }
}
