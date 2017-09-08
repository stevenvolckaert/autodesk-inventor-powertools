namespace StevenVolckaert.InventorPowerTools.Buttons
{
    using System;
    using System.Linq;
    using System.Windows;
    using Inventor;

    internal class CreateTopAndLeftProjectedViewsButton : ButtonBase
    {
        public override string DisplayName { get; } = "Top+Left";

        public override string Description { get; } = "Add Top and Left Projected Views";

        public CreateTopAndLeftProjectedViewsButton()
        {
            Panel = AddIn.UserInterfaceManager.GetRibbonPanel(
                name: "id_PanelD_PlaceViewsCreate",
                tabName: "id_TabPlaceViews",
                ribbonName: "Drawing"
            );
        }

        override protected void OnExecute(NameValueMap context)
        {
            var transaction = AddIn.CreateTransaction(DisplayName);

            try
            {
                // TODO Add extension method that finds a row based on part number.
                //var partsLists = DrawingDocument.ActiveSheet.PartsLists;

                foreach (var drawingView in AddIn.GetDrawingViews("Select a view"))
                {
                    drawingView.AddTopAndLeftProjectedViews(addDimensions: true, drawingDistance: 0.5);
                    AddPartNameNote(drawingView);
                }

                transaction.End();
            }
            catch (Exception ex)
            {
                transaction.Abort();
                MessageBox.Show(ex.ToString());
            }
        }

        private void AddPartNameNote(DrawingView drawingView)
        {
            var partName = drawingView.ReferencedDocumentDescriptor.DisplayName.RemoveExtension();
            var rowItemNumber = GetRowItemNumber(partName);

            if (rowItemNumber.IsNullOrWhiteSpace() == false)
                partName = $"{rowItemNumber} : {partName}";

            drawingView.AddPartName(partName, drawingDistance: 0.5);
        }

        private string GetRowItemNumber(string partNumber)
        {
            foreach (PartsList partsList in DrawingDocument.ActiveSheet.PartsLists)
            {
                var row = partsList.GetRowByPartNumber(partNumber);

                if (row != null)
                    return row.Cast<PartsListCell>().First().Value;
            }

            return null;
        }

        //public void TestFileDialog()
        //{
        //    FileDialog dialog;
        //    Inventor.CreateFileDialog(out(dialog));

        //    dialog.Filter = "Inventor Files (*.iam;*.ipt)|*.iam;*.ipt|All Files (*.*)|*.*";
        //    dialog.FilterIndex = 1;

        //    // Set the title for the dialog.
        //    dialog.DialogTitle = "Open File Test";

        //    // Set the initial directory that will be displayed in the dialog.
        //    //oFileDlg.InitialDirectory = "C:/Temp";

        //    // Set the flag so an error will not be raised if the user clicks the Cancel button.
        //    dialog.CancelError = false;

        //    // Show the open dialog.  The same procedure is also used for the Save dialog.
        //    // The commented code can be used for the Save dialog.
        //    dialog.ShowOpen();
        //    // oFileDlg.ShowSave();

        //    MessageBox.Show("File " + dialog.FileName + " was selected.", "Selected file");
        //}

        //public void TestSelection()
        //{
        //    // Create a new clsSelect object.
        //    var selectObject = new Selector();

        //    //' Call the pick method of the clsSelect object and set
        //    //' the filter to pick any face.
        //    //Dim oFace As Face
        //    //Set oFace = oSelect.Pick(kPartFaceFilter)

        //    //' Check to make sure an object was selected.
        //    //If Not oFace Is Nothing Then
        //    //    ' Display the area of the selected face.
        //    //    MsgBox "Face area: " & oFace.Evaluator.Area & " cm^2"
        //    //End If

        //}
    }
}
