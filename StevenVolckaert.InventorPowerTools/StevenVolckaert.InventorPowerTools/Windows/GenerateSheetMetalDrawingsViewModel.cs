using System;
using System.Collections.Generic;
using System.Linq;
using Inventor;

namespace StevenVolckaert.InventorPowerTools.Windows
{
    internal class GenerateSheetMetalDrawingsViewModel : GenerateDrawingsViewModelBase
    {
        private AssemblyDocument _assembly;
        public AssemblyDocument Assembly
        {
            get { return _assembly; }
            set
            {
                if (_assembly != value)
                {
                    _assembly = value;
                    RaisePropertyChanged(() => Assembly);
                }
            }
        }

        private List<Part> _parts;
        public List<Part> Parts
        {
            get { return _parts; }
            set
            {
                if (_parts != value)
                {
                    _parts = value;
                    RaisePropertyChanged(() => Parts);

                    _documents = value.Cast<IDocument>().ToList();
                    CalculateIsEverythingSelected();
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateSheetMetalDrawingsViewModel"/> class.
        /// </summary>
        public GenerateSheetMetalDrawingsViewModel()
        {
            Title = "Generate Sheet Metal Flat Pattern Drawings";
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

            foreach (var part in selectedParts)
            {
                var drawingDocument = CreateDrawingDocument();
                var dimensionStyle = drawingDocument.StylesManager.ActiveStandardStyle.ActiveObjectDefaults.LinearDimensionStyle;
                var sheet = drawingDocument.ActiveSheet;
                var topRightCorner = sheet.TopRightCorner();

                try
                {
                    // 1. Add flat pattern base view.
                    var flatPatternView = sheet.DrawingViews.AddBaseView(
                        Model: (_Document)part.Document,
                        Position: drawingDocument.ActiveSheet.CenterPoint(),
                        Scale: Scale,
                        ViewOrientation: ViewOrientationTypeEnum.kDefaultViewOrientation,
                        ViewStyle: DrawingViewStyleEnum.kHiddenLineDrawingViewStyle,
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

                    // 2. Add part list to the top right corner.
                    var partsList = sheet.AddPartsList(part.Document, PartsListLevelEnum.kPartsOnly);
                    partsList.PartsListRows[1]["QTY"].Value = Assembly.GetPartQuantity(part.Document).ToString();

                    // 3. Add base "ISO Top Right", Hidden line removed, Shaded base view of the part in the drawing's top right corner.
                    var perspectiveView = sheet.DrawingViews.AddBaseView(
                        Model: (_Document)part.Document,
                        Position: drawingDocument.ActiveSheet.TopRightPoint(),
                        Scale: 0.1,
                        ViewOrientation: ViewOrientationTypeEnum.kIsoTopRightViewOrientation,
                        ViewStyle: DrawingViewStyleEnum.kShadedDrawingViewStyle,
                        ModelViewName: string.Empty,
                        ArbitraryCamera: Type.Missing,
                        AdditionalOptions: Type.Missing
                    );

                    var margin = sheet.Margin();

                    perspectiveView.Fit(
                        new Rectangle(
                            AddIn.CreatePoint2D(
                                ((sheet.Width - margin.Right) * 3 + margin.Left) / 4 + 1,
                                ((sheet.Height - margin.Top) * 3 + margin.Bottom) / 4 + 1
                            ),
                            AddIn.CreatePoint2D(
                                topRightCorner.X - 1,
                                topRightCorner.Y - 1
                            )
                        )
                    );

                    perspectiveView.Position =
                        AddIn.CreatePoint2D(
                            perspectiveView.Position.X,
                            perspectiveView.Position.Y - partsList.RangeBox.Height()
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
