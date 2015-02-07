using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using Inventor;
using Environment = System.Environment;

namespace StevenVolckaert.InventorPowerTools.Windows
{
    internal class GenerateMdfDrawingsViewModel : GenerateDrawingsViewModelBase
    {
        private IList<PartDocument> _mdfParts;
        public IList<PartDocument> MdfParts
        {
            get { return _mdfParts; }
            set
            {
                if (_mdfParts != value)
                {
                    _mdfParts = value;
                    RaisePropertyChanged(() => MdfParts);
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateMdfDrawingsViewModel"/> class.
        /// </summary>
        public GenerateMdfDrawingsViewModel()
        {
            Title = "Generate MDF Drawings";
        }

        protected override void GenerateDrawings()
        {
            foreach (var part in MdfParts)
            {
                var drawingDocument = CreateDrawingDocument();
                var sheet = drawingDocument.ActiveSheet;
                var topRightCorner = sheet.TopRightCorner();

                try
                {
                    // 1. Alter formatting of custom properties.
                    part.SetCustomPropertyFormat("Lengte", CustomPropertyPrecisionEnum.kZeroDecimalPlacePrecision, showUnit: false);
                    part.SetCustomPropertyFormat("Breedte", CustomPropertyPrecisionEnum.kZeroDecimalPlacePrecision, showUnit: false);
                    part.SetCustomPropertyFormat("Dikte", CustomPropertyPrecisionEnum.kZeroDecimalPlacePrecision, showUnit: false);

                    // 2. Add base view.
                    var baseView = sheet.DrawingViews.AddBaseView(
                        Model: (_Document)part,
                        Position: drawingDocument.ActiveSheet.CenterPoint(),
                        Scale: 0.2,
                        ViewOrientation: ViewOrientationTypeEnum.kDefaultViewOrientation,
                        ViewStyle: DrawingViewStyleEnum.kHiddenLineDrawingViewStyle,
                        ModelViewName: string.Empty,
                        ArbitraryCamera: Type.Missing,
                        AdditionalOptions: Type.Missing
                    );

                    baseView.AddTopAndLeftProjectedViews(
                        addDimensions: true,
                        drawingDistance: 0.5
                    );

                    // 3. Add part list to the top right corner.
                    var partsList = sheet.AddPartsList(part, PartsListLevelEnum.kPartsOnly);

                    // 4. Add base "ISO TOP Right", Hidden line removed, Shaded base view of the subassembly in the drawing's top right corner.
                    //var perspectiveView = sheet.DrawingViews.AddBaseView(
                    //    Model: (_Document)part,
                    //    Position: drawingDocument.ActiveSheet.TopRightPoint(),
                    //    Scale: 0.1,
                    //    ViewOrientation: ViewOrientationTypeEnum.kIsoTopRightViewOrientation,
                    //    ViewStyle: DrawingViewStyleEnum.kShadedDrawingViewStyle,
                    //    ModelViewName: string.Empty,
                    //    ArbitraryCamera: Type.Missing,
                    //    AdditionalOptions: Type.Missing
                    //);

                    //var margin = sheet.Margin();

                    //perspectiveView.Fit(
                    //    new Rectangle(
                    //        AddIn.CreatePoint2d(
                    //            ((sheet.Width - margin.Right) * 3 + margin.Left) / 4 + 1,
                    //            ((sheet.Height - margin.Top) * 3 + margin.Bottom) / 4 + 1
                    //        ),
                    //        AddIn.CreatePoint2d(
                    //            topRightCorner.X - 1,
                    //            topRightCorner.Y - 1
                    //        )
                    //    )
                    //);

                    //perspectiveView.Position =
                    //    AddIn.CreatePoint2d(
                    //        perspectiveView.Position.X,
                    //        perspectiveView.Position.Y - partsList.RangeBox.Height()
                    //    );
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        messageBoxText: string.Format(CultureInfo.InvariantCulture, "Encountered an exception when generating drawings for part document {0}:{1}", part.FullFileName, Environment.NewLine + ex.ToString()),
                        caption: "Exception",
                        button: MessageBoxButton.OK,
                        icon: MessageBoxImage.Exclamation
                    );
                }
            }
        }
    }
}
