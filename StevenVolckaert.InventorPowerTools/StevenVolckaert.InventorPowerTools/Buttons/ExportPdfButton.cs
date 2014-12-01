using System;
using System.Linq;
using System.Windows;
using Inventor;

namespace StevenVolckaert.InventorPowerTools.Buttons
{
    internal class ExportPdfButton : ButtonBase
    {
        private string _targetDirectoryPath;

        public override string DisplayName
        {
            get { return "PDF"; }
        }

        public override string Description
        {
            get { return "Export all open drawings as PDF"; }
        }

        public ExportPdfButton()
        {
            //Panel = AddIn.UserInterfaceManager.GetPanel("id_PanelD_PlaceViewsCreate", tabName: "id_TabPlaceViews", ribbonName: "Drawing");
        }

        protected override void OnExecute(NameValueMap context)
        {
            try
            {
                var drawingDocuments = AddIn.Documents.OfType<DrawingDocument>().ToList();

                if (drawingDocuments.Count == 0)
                {
                    ShowWarningMessageBox("There are no drawing documents loaded. Open one or more drawing documents before using this command.");
                    return;
                }

                _targetDirectoryPath = ((Document)drawingDocuments.First()).DirectoryPath();

                using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
                {
                    dialog.Description = "Select a target directory.";
                    dialog.SelectedPath = _targetDirectoryPath;
                    dialog.ShowNewFolderButton = true;

                    if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                        return;

                    _targetDirectoryPath = dialog.SelectedPath + "\\";
                }

                foreach (Document document in drawingDocuments)
                    document.SaveAsPDF(_targetDirectoryPath + document.DisplayName + ".pdf");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
