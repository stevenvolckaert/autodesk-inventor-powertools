namespace StevenVolckaert.InventorPowerTools.Windows
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;
    using Inventor;
    using Microsoft.Practices.Prism.Commands;

    internal abstract class GenerateDrawingsViewModelBase : ViewModelBase
    {
        protected List<IDocument> _documents;

        private string _templateFileName = AddIn.TemplatesPath + "Metric\\ISO.idw";
        /// <summary>
        ///     Gets or sets the file name of the template.
        /// </summary>
        public string TemplateFileName
        {
            get { return _templateFileName; }
            set
            {
                if (_templateFileName == value)
                    return;

                _templateFileName = value;
                RaisePropertyChanged(() => TemplateFileName);
            }
        }

        private double _scale = 0.2;
        public double Scale
        {
            get { return _scale; }
            set
            {
                if (_scale == value)
                    return;

                _scale = value;
                RaisePropertyChanged(() => Scale);
            }
        }

        private double _perspectiveScale = 0.1;
        public double PerspectiveScale
        {
            get { return _perspectiveScale; }
            set
            {
                if (_perspectiveScale == value)
                    return;

                _perspectiveScale = value;
                RaisePropertyChanged(() => PerspectiveScale);
            }
        }

        public IList<LinearPrecision> SupportedLinearPrecisions { get; }
            = LinearPrecision.CreateFractionalLinearPrecisions().ToList();

        private LinearPrecision _selectedLinearPrecision;
        public LinearPrecision SelectedLinearPrecision
        {
            get
            {
                return _selectedLinearPrecision ?? (
                    _selectedLinearPrecision = SupportedLinearPrecisions
                        .First(x => x.EnumValue == LinearPrecisionEnum.kHalfFractionalLinearPrecision)
                    );
            }
            set
            {
                if (_selectedLinearPrecision != value)
                {
                    _selectedLinearPrecision = value;
                    RaisePropertyChanged(() => SelectedLinearPrecision);
                }
            }
        }

        private bool _areTrailingZerosDisplayed = false;
        public bool AreTrailingZerosDisplayed
        {
            get { return _areTrailingZerosDisplayed; }
            set
            {
                if (_areTrailingZerosDisplayed != value)
                {
                    _areTrailingZerosDisplayed = value;
                    RaisePropertyChanged(() => AreTrailingZerosDisplayed);
                }
            }
        }

        private bool? _isEverythingSelected;
        public bool? IsEverythingSelected
        {
            get { return _isEverythingSelected; }
            set
            {
                if (_isEverythingSelected != value)
                {
                    _isEverythingSelected = value;
                    RaisePropertyChanged(() => IsEverythingSelected);

                    if (value.HasValue)
                        _documents.ForEach(x => x.IsSelected = value.Value);
                }
            }
        }

        public void ComputeIsEverythingSelected()
        {
            if (_documents.All(x => x.IsSelected == true))
                _isEverythingSelected = true;
            else if (_documents.All(x => x.IsSelected == false))
                _isEverythingSelected = false;
            else
                _isEverythingSelected = null;

            RaisePropertyChanged(() => IsEverythingSelected);
        }

        public ICommand SelectTemplateCommand { get; private set; }
        public ICommand GenerateDrawingsCommand { get; private set; }

        protected override void RegisterCommands()
        {
            SelectTemplateCommand = new DelegateCommand(SelectTemplate);
            GenerateDrawingsCommand = new DelegateCommand(GenerateDrawings);
        }

        private void SelectTemplate()
        {
            using (var dialog = new System.Windows.Forms.OpenFileDialog())
            {
                dialog.Filter = "Inventor Drawings (*.idw)|*.idw";
                dialog.InitialDirectory = System.IO.Path.GetDirectoryName(TemplateFileName);
                dialog.RestoreDirectory = true;
                dialog.Multiselect = false;

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    TemplateFileName = dialog.FileName;
            }
        }

        protected abstract void GenerateDrawings();

        /// <summary>
        ///     Creates a new <see cref="DrawingDocument"/>, using the template specified by the
        ///     <seealso cref="TemplateFileName"/> property.
        /// </summary>
        /// <returns>A new <see cref="DrawingDocument"/> instance.</returns>
        protected DrawingDocument CreateDrawingDocument()
        {
            // TODO Create a unit test that tests whether setting the LinearPrecision of the LinearDimensionStyle
            // property (see below) works as expected. Not sure if unit tests are possible without running Inventor,
            // though. Steven Volckaert. September 29, 2016.

            var drawingDocument =
                AddIn.CreateDrawingDocument(
                    templateFileName: TemplateFileName,
                    isVisible: true
                );

            var activeLinearDimensionStyle = drawingDocument.ActiveLinearDimensionStyle();

            activeLinearDimensionStyle.TrailingZeroDisplay = AreTrailingZerosDisplayed;
            activeLinearDimensionStyle.LinearPrecision =
                SelectedLinearPrecision?.EnumValue ?? LinearPrecisionEnum.kZeroFractionalLinearPrecision;

            return drawingDocument;
        }

        protected void SetCustomPropertyFormat(Part part)
        {
            part.SetCustomPropertyFormat(SelectedLinearPrecision, AreTrailingZerosDisplayed);
        }

        protected void SetCustomPropertyFormat(IEnumerable<Part> parts)
        {
            foreach (var part in parts)
                SetCustomPropertyFormat(part);
        }
    }
}
