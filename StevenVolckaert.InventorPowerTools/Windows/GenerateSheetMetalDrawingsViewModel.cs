namespace StevenVolckaert.InventorPowerTools.Windows
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Drawing;
    using Inventor;

    internal class GenerateSheetMetalDrawingsViewModel : GenerateDrawingsViewModelBase
    {
        private AssemblyDocument _assembly;
        public AssemblyDocument Assembly
        {
            get { return _assembly; }
            set
            {
                if (_assembly == value)
                    return;

                _assembly = value;
                RaisePropertyChanged(() => Assembly);
            }
        }

        private List<Part> _parts;
        public List<Part> Parts
        {
            get { return _parts; }
            set
            {
                if (_parts == value)
                    return;

                _parts = value;
                RaisePropertyChanged(() => Parts);

                _documents = value.Cast<IDocument>().ToList();
                ComputeIsEverythingSelected();
            }
        }

        public IList<IGenerateSheetMetalDrawingsBehavior> SupportedGenerateDrawingsBehaviors { get; } =
            new List<IGenerateSheetMetalDrawingsBehavior>
            {
                new GenerateSheetMetalFlatPatternDrawingsBehavior(),
                new GenerateBaseViewWithLeftThenThreeTopProjectedViewsDrawingsBehavior()
            };

        private IGenerateSheetMetalDrawingsBehavior _selectedGenerateDrawingsBehavior;
        public IGenerateSheetMetalDrawingsBehavior SelectedGenerateDrawingsBehavior
        {
            get
            {
                return _selectedGenerateDrawingsBehavior
                    ?? (_selectedGenerateDrawingsBehavior = SupportedGenerateDrawingsBehaviors.First());
            }
            set
            {
                if (_selectedGenerateDrawingsBehavior == value)
                    return;

                _selectedGenerateDrawingsBehavior = value;
                RaisePropertyChanged(() => SelectedGenerateDrawingsBehavior);
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="GenerateSheetMetalDrawingsViewModel"/> class.
        /// </summary>
        public GenerateSheetMetalDrawingsViewModel()
        {
            Title = "Generate Sheet Metal Flat Pattern Drawings";
            SelectedViewStyle =
                SupportedViewStyles.First(
                    x => x.EnumValue == DrawingViewStyleEnum.kHiddenLineDrawingViewStyle
                );
        }

        protected override void GenerateDrawings()
        {
            if (Parts == null)
                return;

            var selectedParts = Parts.Where(x => x.IsSelected == true).ToList();

            if (selectedParts.Count == 0)
            {
                ShowWarningMessageBox("No sheet metal parts are selected.");
                return;
            }

            var bom = Assembly.ComponentDefinition.BOM;

            if (bom?.RequiresUpdate == true)
                AddIn.ShowWarningMessageBox(
                    caption: Title,
                    message: $"The BOM of assembly '{Assembly.DisplayName}' requires an update."
                        + System.Environment.NewLine + System.Environment.NewLine
                        + "Quantities displayed in the generated drawings might be incorrect."
                );

            foreach (var part in selectedParts)
            {
                var drawingDocument = CreateDrawingDocument();

                var dimensionStyle = drawingDocument
                    .StylesManager.ActiveStandardStyle.ActiveObjectDefaults.LinearDimensionStyle;

                var sheet = drawingDocument.ActiveSheet;

                try
                {
                    SetCustomPropertyFormat(part);

                    SelectedGenerateDrawingsBehavior.AddViews(part, drawingDocument);
                    continue;



                    // 2. Add flat pattern base view.
                    var flatPatternView = sheet.DrawingViews.AddBaseView(
                        Model: (_Document)part.Document,
                        Position: drawingDocument.ActiveSheet.CenterPoint(),
                        Scale: Scale,
                        ViewOrientation: ViewOrientationTypeEnum.kDefaultViewOrientation,
                        ViewStyle: SelectedViewStyle.EnumValue,
                        ModelViewName: string.Empty,
                        ArbitraryCamera: Type.Missing,
                        AdditionalOptions: AddIn.CreateNameValueMap("SheetMetalFoldedModel", false)
                    );

                    flatPatternView.AddHorizontalBendLineDimensionSet(dimensionStyle);
                    flatPatternView.AddVerticalBendLineDimensionSet(dimensionStyle);

                    if (flatPatternView.VerticalLines().Any(x => x.IsBendLine()))
                        flatPatternView.AddHorizontalDimension(dimensionStyle, drawingDistance: 2.0);

                    if (flatPatternView.HorizontalLines().Any(x => x.IsBendLine()))
                        flatPatternView.AddVerticalDimension(dimensionStyle, drawingDistance: 2.0);

                    // 3. Add part list to the top right corner.
                    var partsList = sheet.AddPartsList(part.Document, PartsListLevelEnum.kPartsOnly);
                    var quantity = Assembly.GetPartQuantity(part.Document);

                    if (quantity > 0)
                        partsList.PartsListRows[1]["QTY"].Value = quantity.ToString();

                    // 4. Add base "ISO Top Right", base view of the part in the drawing's top right corner.
                    var perspectiveView = sheet.DrawingViews.AddBaseView(
                        Model: (_Document)part.Document,
                        Position: drawingDocument.ActiveSheet.TopRightPoint(),
                        Scale: 0.1,
                        ViewOrientation: ViewOrientationTypeEnum.kIsoTopRightViewOrientation,
                        ViewStyle: SelectedViewStyle.EnumValue,
                        ModelViewName: string.Empty,
                        ArbitraryCamera: Type.Missing,
                        AdditionalOptions: Type.Missing
                    );

                    perspectiveView.FitToTopRightCorner(sheet);
                    perspectiveView.Position =
                        AddIn.CreatePoint2D(
                            x: perspectiveView.Position.X,
                            y: perspectiveView.Position.Y - partsList.RangeBox.Height()
                        );

                    // 5. TODO Add 'Top View' below the 'ISO Top Right' view.
                    // TODO Implement extension method 'BottomRightCorner()'
                    // 
                    // not necessary for GenerateSheetMetalDrawingsBehaviorType.BaseViewWithLeftThenThreeTopProjectedViews
                    var margin = sheet.Margin();

                    var topView = sheet.DrawingViews.AddBaseView(
                        Model: (_Document)part.Document,
                        Position: drawingDocument.ActiveSheet.BottomLeftCorner(),
                        Scale: 0.1,
                        ViewOrientation: ViewOrientationTypeEnum.kTopViewOrientation,
                        ViewStyle: SelectedViewStyle.EnumValue,
                        ModelViewName: string.Empty,
                        ArbitraryCamera: Type.Missing,
                        AdditionalOptions: Type.Missing
                    );

                    topView.Position =
                        AddIn.CreatePoint2D(
                            x: margin.Left + topView.Width + 1,
                            y: margin.Bottom + topView.Height + 1
                        );
                }
                catch (Exception ex)
                {
                    ShowWarningMessageBox(ex.ToString());
                }
            }
        }
    }
}
