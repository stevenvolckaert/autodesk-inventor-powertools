namespace StevenVolckaert.InventorPowerTools
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Windows;
    using Buttons;
    using Inventor;

    /// <summary>
    /// Represents an Autodesk Inventor add-in.
    /// </summary>
    [Guid(AssemblyInfo.Guid)]
    public class AddIn : ApplicationAddInServer
    {
        /// <summary>
        /// The client ID of the add-in.
        /// </summary>
        public const string ClientId = "{" + AssemblyInfo.Guid + "}";

        private CreateTopAndLeftProjectedViewsButton _createTopAndLeftViewButton;
        private CreatePartViewsFromAssemblyButton _createPartViewsFromAssemblyButton;
        private ExportPdfButton _exportPdfButton;
        private GenerateSheetMetalDrawingsButton _generateSheetMetalDrawingsButton;
        private GenerateMdfDrawingsButton _generateMdfDrawingsButton;
        private GenerateSubassemblyDrawingsButton _generateSubAssemblyDrawingsButton;

        public static Inventor.Application Inventor { get; private set; }

        #region Helper properties

        /// <summary>
        /// Returns all in-memory Inventor documents.
        /// </summary>
        public static Documents Documents
        {
            get { return Inventor.Documents; }
        }

        public static _Document ActiveDocument
        {
            get { return Inventor.ActiveDocument; }
        }

        public static UserInterfaceManager UserInterfaceManager
        {
            get { return Inventor.UserInterfaceManager; }
        }

        public static UserInterfaceEvents UserInterfaceEvents
        {
            get { return Inventor.UserInterfaceManager.UserInterfaceEvents; }
        }

        private static GeneralPreferences _generalPreferences;
        public static GeneralPreferences GeneralPreferences
        {
            get { return _generalPreferences ?? (_generalPreferences = Inventor.Preferences.OfType<GeneralPreferences>().FirstOrDefault()); }
        }

        /// <summary>
        /// Gets the currently selected drawing views.
        /// </summary>
        private static IEnumerable<DrawingView> SelectedDrawingViews
        {
            get { return ((DrawingDocument)ActiveDocument).SelectSet.OfType<DrawingView>(); }
        }

        /// <summary>
        /// Gets or sets the location of template files that Autodesk Inventor uses when creating new files. 
        /// </summary>
        public static string TemplatesPath
        {
            get { return Inventor.FileOptions.TemplatesPath; }
            set { Inventor.FileOptions.TemplatesPath = value; }
        }

        #endregion

        #region Helper methods

        // TODO Place ToValueList<TEnum>() in an appropriate static class.
        // As extension methods require an instance to be called, its place is not here.
        // (We're never using the value parameter below.) 
        // See http://stackoverflow.com/a/1167367/2314596

        /// <summary>
        /// Creates a list of all values defined by the specified enumeration type.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enumeration values.</typeparam>
        /// <returns>A list containing all values defined by the enumeration type.</returns>
        public static IList<TEnum> CreateEnumValueList<TEnum>()
            where TEnum : struct, IConvertible
        {
            var enumType = typeof(TEnum);

            if (!enumType.IsEnum)
                throw new ArgumentException(
                    string.Format(CultureInfo.CurrentCulture, Resources.IllegalType, enumType, nameof(Enum))
                );

            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToList();
        }

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
        public static Point2d CreatePoint2D(double x, double y)
        {
            return Inventor.TransientGeometry.CreatePoint2d(x, y);
        }

        /// <summary>
        /// Creates a new NameValueMap object with a single parameter.
        /// </summary>
        /// <param name="key">The parameter's key.</param>
        /// <param name="value">The parameter's value.</param>
        public static NameValueMap CreateNameValueMap(string key, object value)
        {
            return CreateNameValueMap(new Dictionary<string, object> { { key, value } });
        }

        /// <summary>
        /// Creates a new NameValueMap object.
        /// </summary>
        /// <param name="parameters">The value map's parameters.</param>
        public static NameValueMap CreateNameValueMap(IDictionary<string, object> parameters)
        {
            var nameValueMap = Inventor.TransientObjects.CreateNameValueMap();

            if (parameters != null)
                foreach (var keyValuePair in parameters)
                    nameValueMap.Add(keyValuePair.Key, keyValuePair.Value);

            return nameValueMap;
        }

        /// <summary>
        /// Creates a new ObjectCollection object.
        /// </summary>
        /// <param name="content">The ObjectCollection's content.</param>
        public static ObjectCollection CreateObjectCollection(params object[] content)
        {
            var objectCollection = Inventor.TransientObjects.CreateObjectCollection();

            foreach (var arg in content.Where(x => x != null))
                objectCollection.Add(arg);

            return objectCollection;
        }

        /// <summary>
        /// Creates a new drawing document with a single sheet.
        /// </summary>
        /// <param name="templateFileName">The file name of the drawing document template to use.</param>
        /// <param name="sheetSize">The size of the drawing's sheet.</param>
        /// <param name="isVisible">A value that indicates whether the new document is visible.</param>
        /// <returns>The new drawing document.</returns>
        public static DrawingDocument CreateDrawingDocument(string templateFileName, DrawingSheetSizeEnum sheetSize, bool isVisible)
        {
            var drawingDocument = CreateDrawingDocument(templateFileName, isVisible);

            foreach (Sheet sheet in drawingDocument.Sheets)
                sheet.Size = sheetSize;

            //if (isVisible)
            //    Inventor.ActiveView.Fit();

            //drawingDocument.SaveAs(
            //    FileName: "",
            //    SaveCopyAs: false
            //);

            //drawingDocument.Sheets.Add

            //drawingDocument.Sheets.AddUsingSheetFormat(
            //    SheetFormat:
            //    Model:
            //    SheetName: "",
            //    AdditionalOptions:
            //    WeldmentFeatureGroup:
            //    SheetMetalFoldedModel: false // If the model document type is a sheet metal part, this option specifies if the view is to be created from the folded model (True) or the flattened model (False). The default value if not specified is to display the folded model. This option is ignored if the model document is not a sheet metal part. 
            //);

            // NOTE: AdditionalOptions can also be used (NameValueMap) to supply various parameters (e.g. SheetMetalFoldedModel).

            return drawingDocument;
        }

        /// <summary>
        /// Creates a new drawing document with a single sheet.
        /// </summary>
        /// <param name="templateFileName">The file name of the drawing document template to use.</param>
        /// <param name="isVisible">A value that indicates whether the new document is visible.</param>
        /// <returns>The new drawing document.</returns>
        public static DrawingDocument CreateDrawingDocument(string templateFileName, bool isVisible)
        {
            if (!System.IO.Path.IsPathRooted(templateFileName))
                templateFileName = GeneralPreferences.TemplateDir + templateFileName;

            return (DrawingDocument)Inventor.Documents.Add(
                DocumentType: DocumentTypeEnum.kDrawingDocumentObject,
                TemplateFileName: templateFileName,
                CreateVisible: isVisible
            );
        }

        /// <summary>
        /// Creates a new transaction on the active document, and starts it.
        /// </summary>
        /// <param name="displayName">The transaction's display name.</param>
        /// <returns>The transaction.</returns>
        public static Transaction CreateTransaction(string displayName)
        {
            return AddIn.Inventor.TransactionManager.StartTransaction(ActiveDocument, displayName);
        }

        /// <summary>
        /// Returns the first selected AssemblyDocument in the current scope, or lets the user select one if the active document is a drawing.
        /// </summary>
        /// <returns>The assembly document, or <c>null</c> if the user didn't select one.</returns>
        /// <exception cref="InvalidOperationException">The active document is not an assembly or drawing document.</exception>
        public static AssemblyDocument GetAssemblyDocument()
        {
            AssemblyDocument assemblyDocument = null;

            switch (AddIn.ActiveDocument.DocumentType)
            {
                case DocumentTypeEnum.kAssemblyDocumentObject:
                    assemblyDocument = (AssemblyDocument)AddIn.ActiveDocument;
                    break;
                case DocumentTypeEnum.kDrawingDocumentObject:
                    var selectedView = AddIn.GetDrawingView("Select an assembly view");

                    if (selectedView == null)
                        break;

                    assemblyDocument = selectedView.ReferencedDocumentDescriptor.ReferencedDocument as AssemblyDocument;

                    if (assemblyDocument == null)
                        MessageBox.Show("View " + selectedView.ReferencedDocumentDescriptor.DisplayName + " is not an assembly view.");

                    break;
                default:
                    throw new InvalidOperationException("The active document is not an assembly or drawing document.");
            }

            return assemblyDocument;
        }

        /// <summary>
        /// Returns the first selected drawing view, or lets the user select one in the active document.
        /// </summary>
        /// <param name="promptText">The text to display as prompt.</param>
        /// <returns>The selected drawing view, or <c>null</c> if the user didn't select one.</returns>
        /// <exception cref="InvalidOperationException">The active document isn't a drawing document.</exception>
        public static DrawingView GetDrawingView(string promptText)
        {
            if (ActiveDocument.DocumentType != DocumentTypeEnum.kDrawingDocumentObject)
                throw new InvalidOperationException("The currently active document isn't a drawing document.");

            return SelectedDrawingViews.FirstOrDefault() ?? SelectDrawingView(promptText);
        }

        /// <summary>
        /// Returns the currently selected drawing views, or lets the user select one in the active document.
        /// </summary>
        /// <param name="promptText">The text to display as prompt.</param>
        /// <returns>The selected drawing view(s), or an empty list if the user didn't select one.</returns>
        /// <exception cref="InvalidOperationException">The active document isn't a drawing document.</exception>
        public static List<DrawingView> GetDrawingViews(string promptText)
        {
            if (ActiveDocument.DocumentType != DocumentTypeEnum.kDrawingDocumentObject)
                throw new InvalidOperationException("The currently active document isn't a drawing document.");

            var selectedDrawingViews = SelectedDrawingViews.ToList();

            if (selectedDrawingViews.Count == 0)
            {
                var selectedDrawingView = SelectDrawingView(promptText);

                if (selectedDrawingView != null)
                    selectedDrawingViews.Add(selectedDrawingView);
            }

            return selectedDrawingViews;
        }

        /// <summary>
        /// Lets the user select a single drawing view in the active document.
        /// </summary>
        /// <param name="promptText">The text to display as prompt.</param>
        /// <returns>The selected drawing view.</returns>
        /// <exception cref="InvalidOperationException">The active document isn't a drawing document.</exception>
        public static DrawingView SelectDrawingView(string promptText)
        {
            if (ActiveDocument.DocumentType != DocumentTypeEnum.kDrawingDocumentObject)
                throw new InvalidOperationException("The currently active document isn't a drawing document.");

            return (DrawingView)Inventor.CommandManager.Pick(SelectionFilterEnum.kDrawingViewFilter, promptText);
        }

        #endregion

        #region ApplicationAddInServer members

        public void Activate(Inventor.ApplicationAddInSite AddInSiteObject, bool FirstTime)
        {
            Inventor = AddInSiteObject.Application;
            UserInterfaceEvents.OnResetCommandBars += OnResetCommandBars;
            UserInterfaceEvents.OnEnvironmentChange += OnEnvironmentChange;
            UserInterfaceEvents.OnResetRibbonInterface += OnResetRibbonInterface;

            try
            {
                _createTopAndLeftViewButton = new CreateTopAndLeftProjectedViewsButton();
                _createPartViewsFromAssemblyButton = new CreatePartViewsFromAssemblyButton();
                _exportPdfButton = new ExportPdfButton();
                _generateSheetMetalDrawingsButton = new GenerateSheetMetalDrawingsButton();
                _generateMdfDrawingsButton = new GenerateMdfDrawingsButton();
                _generateSubAssemblyDrawingsButton = new GenerateSubassemblyDrawingsButton();

                if (FirstTime == true)
                    if (UserInterfaceManager.InterfaceStyle == InterfaceStyleEnum.kClassicInterface)
                    {
                        //// Create a new command bar
                        //var slotCommandBar = Runtime.UserInterfaceManager.CommandBars.Add("Slot", "Autodesk:SimpleAddIn:SlotToolbar", CommandBarTypeEnum.kRegularCommandBar, Runtime.AddInId);
                        //slotCommandBar.Controls.AddButton(_createTopAndLeftViewButton.ButtonDefinition, 0);

                        //// Make the command bar accessible in the panel menu for the 2d sketch environment.
                        //Runtime.UserInterfaceManager.Environments["PMxPartSketchEnvironment"].PanelBar.CommandBarList.Add(slotCommandBar);
                    }
                    else
                        RegisterButtons();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void RegisterButtons()
        {
            var generateDrawingsPanel = Inventor.UserInterfaceManager.AddPanel(
                name: "Autodesk:PowerTools:GenerateDrawingsRibbonPanel",
                displayName: "Generate Drawings",
                tabName: "id_TabTools",
                ribbonName: "Assembly"
            );

            var exportPanel = Inventor.UserInterfaceManager.AddPanel(
                name: "Autodesk:PowerTools:ExportPanel",
                displayName: "Export",
                tabName: "id_TabTools",
                ribbonName: "Drawing"
            );

            _createTopAndLeftViewButton.Panel.CommandControls.AddSeparator();
            _createTopAndLeftViewButton.AddToPanel();
            _createPartViewsFromAssemblyButton.AddToPanel();
            _generateSheetMetalDrawingsButton.AddTo(generateDrawingsPanel);
            _generateMdfDrawingsButton.AddTo(generateDrawingsPanel);
            _generateSubAssemblyDrawingsButton.AddTo(generateDrawingsPanel);
            _exportPdfButton.AddTo(exportPanel);
        }

        public void Deactivate()
        {
            UserInterfaceEvents.OnResetCommandBars -= OnResetCommandBars;
            UserInterfaceEvents.OnEnvironmentChange -= OnEnvironmentChange;
            UserInterfaceEvents.OnResetRibbonInterface -= OnResetRibbonInterface;

            _createPartViewsFromAssemblyButton.Dispose();
            _createTopAndLeftViewButton.Dispose();
            _createPartViewsFromAssemblyButton = null;
            _createTopAndLeftViewButton = null;

            Marshal.ReleaseComObject(Inventor);
            Inventor = null;

            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void OnResetCommandBars(ObjectsEnumerator commandBars, NameValueMap context)
        {
            try
            {
                foreach (CommandBar commandBar in commandBars)
                    if (commandBar.InternalName == "PMxPartFeatureCmdBar")
                    {
                        _createTopAndLeftViewButton.AddTo(commandBar);
                        _createPartViewsFromAssemblyButton.AddTo(commandBar);
                        return;
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void OnEnvironmentChange(Inventor.Environment environment, EnvironmentStateEnum environmentState, EventTimingEnum beforeOrAfter, NameValueMap context, out HandlingCodeEnum handlingCode)
        {
            try
            {
                var isEnabled = environmentState == EnvironmentStateEnum.kActivateEnvironmentState || environmentState == EnvironmentStateEnum.kResumeEnvironmentState;

                switch (environment.InternalName)
                {
                    case "DLxDrawingEnvironment":
                        _createTopAndLeftViewButton.IsEnabled = isEnabled;
                        _createPartViewsFromAssemblyButton.IsEnabled = isEnabled;
                        _exportPdfButton.IsEnabled = isEnabled;
                        break;
                    case "AMxAssemblyEnvironment":
                        _generateSheetMetalDrawingsButton.IsEnabled = isEnabled;
                        _generateMdfDrawingsButton.IsEnabled = isEnabled;
                        _generateSubAssemblyDrawingsButton.IsEnabled = isEnabled;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            handlingCode = HandlingCodeEnum.kEventNotHandled;
        }

        private void OnResetRibbonInterface(NameValueMap context)
        {
            RegisterButtons();
        }

        public void ExecuteCommand(int commandID)
        {
            // NOTE: This method is obsolete. Use the ControlDefinition instances to implement commands.
        }

        public object Automation
        {
            // This property is provided to allow the AddIn to expose an API 
            // of its own to other programs. Typically, this  would be done by
            // implementing the AddIn's API interface in a class and returning 
            // that class object through this property.

            get { return null; }
        }

        #endregion

        #region MessageBox

        public static void ShowMessageBox(string caption, string message)
        {
            ShowMessageBox("{0}", message);
        }

        public static void ShowMessageBox(string caption, string messageFormat, params object[] messageArgs)
        {
            MessageBox.Show(
                messageBoxText: string.Format(CultureInfo.InvariantCulture, messageFormat, messageArgs),
                caption: caption,
                button: MessageBoxButton.OK,
                icon: MessageBoxImage.Information
            );
        }

        public static void ShowWarningMessageBox(string caption, Exception exception)
        {
            ShowWarningMessageBox("{0}", exception.ToString());
        }

        public static void ShowWarningMessageBox(string caption, string messageFormat, params object[] messageArgs)
        {
            MessageBox.Show(
                messageBoxText: string.Format(CultureInfo.InvariantCulture, messageFormat, messageArgs),
                caption: caption,
                button: MessageBoxButton.OK,
                icon: MessageBoxImage.Exclamation
            );
        }

        #endregion

        //private void OnSaveDocument(_Document DocumentObject, EventTimingEnum BeforeOrAfter, NameValueMap Context, HandlingCodeEnum HandlingCode)
        //{
        //    switch (BeforeOrAfter)
        //    {
        //        case EventTimingEnum.kAbort:
        //        case EventTimingEnum.kAfter:
        //            break;
        //        case EventTimingEnum.kBefore:
        //            try
        //            {
        //                if (Inventor.ActiveDocumentType == DocumentTypeEnum.kDrawingDocumentObject)
        //                {
        //                    var propertySet = ActiveDocument.PropertySets["{D5CDD505-2E9C-101B-9397-08002B2CF9AE}"];
        //                    propertySet.SetValue("SysDate", String.Format("MMM-d-yy", DateTime.Now));
        //                    propertySet.SetValue("SysTime", String.Format("hh:mm:ss tt", DateTime.Now));
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show(ex.ToString());
        //            }
        //            break;
        //    }
        //}

        //private void btnProcess_Click(Object sender, EventArgs e)
        //{
        //    string path = string.Empty;
        //    System.Windows.Controls.ListBox lstResults = new System.Windows.Controls.ListBox();

        //    // Validate the input path.
        //    if (!System.IO.Directory.Exists(path))
        //    {
        //        MessageBox.Show("You must specify a valid path.");
        //        return;
        //    }

        //    // Get all of the drawing files in the directory and subdirectories.
        //    lstResults.Items.Clear();
        //    lstResults.Items.Add("Finding drawing files in specified directory...");

        //    var drawings = System.IO.Directory.GetFiles(path, "*.idw", System.IO.SearchOption.AllDirectories);
        //    lstResults.Items.Clear();
        //    //lstResults.Refresh();
        //    lstResults.Items.Add("=== Processing directory: " + path + " ===");

        //    foreach (var drawing in drawings)
        //    {
        //        // Skip any drawings in an "OldVersions" directory.
        //        if (!drawing.Contains("OldVersions"))
        //        {
        //            lstResults.Items.Add("Processing: .\\" + drawing.Substring(path.Length + 1));

        //            // Make sure the list is scrolled so the bottom of the list can be seen.
        //            //lstResults.TopIndex = lstResults.Items.Count - 1;
        //            //lstResults.Refresh();

        //            // Open the drawing in Inventor
        //            Inventor.DrawingDocument drawDoc = null;
        //            try
        //            {
        //                Inventor.SilentOperation = true;
        //                drawDoc = (DrawingDocument)Inventor.Documents.Open(drawing);
        //                Inventor.SilentOperation = false;
        //            }
        //            catch
        //            {
        //            }

        //            if (drawDoc != null)
        //            {
        //                if (drawDoc.SaveAsPDF(System.IO.Path.ChangeExtension(drawing, "pdf")))
        //                    lstResults.Items[lstResults.Items.Count - 1] = "Successfully processed: .\\" + drawing.Substring(path.Length + 1);
        //                else
        //                    lstResults.Items[lstResults.Items.Count - 1] = "Error while processing: .\\" + drawing.Substring(path.Length + 1);
        //            }
        //            else
        //                lstResults.Items[lstResults.Items.Count - 1] = "Error while processing: .\\" + drawing.Substring(path.Length + 1);

        //            //lstResults.TopIndex = lstResults.Items.Count - 1;
        //            //lstResults.Refresh();

        //            if (drawDoc != null)
        //                drawDoc.Close(true);
        //        }
        //    }

        //    lstResults.Items.Add("=== Processing complete. ===");

        //    // Make sure the list is scrolled so the bottom of the list can be seen.
        //    //lstResults.TopIndex = lstResults.Items.Count - 1;
        //    //lstResults.Refresh();
        //}
    }
}
