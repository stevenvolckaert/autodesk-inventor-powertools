namespace StevenVolckaert.InventorPowerTools.Buttons
{
    using System;
    using Inventor;
    using Windows;
    using Environment = System.Environment;

    internal class GenerateSubassemblyDrawingsButton : ButtonBase
    {
        private readonly GenerateSubassemblyDrawingsWindow _generateDrawingsWindow = new GenerateSubassemblyDrawingsWindow();

        public override string DisplayName { get; } = "Subassembly";

        public override string Description { get; } =
            "Generate a drawing of every" + Environment.NewLine + "subassembly in the active document.";

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
                        _generateDrawingsWindow.Title,
                        $"Assembly '{assembly.FullFileName}' doesn't contain subassemblies."
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
