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
            Title = "Generate Sheet Metal Drawings";
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

                try
                {
                    SetCustomPropertyFormat(part);

                    SelectedGenerateDrawingsBehavior.AddViews(
                        part: part,
                        quantity: Assembly.GetPartQuantity(part.Document),
                        drawingDocument: drawingDocument,
                        viewStyle: SelectedViewStyle,
                        scale: Scale,
                        perspectiveScale: PerspectiveScale
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
