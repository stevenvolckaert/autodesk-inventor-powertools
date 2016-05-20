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
        /// Gets or sets the file name of the template.
        /// </summary>
        public string TemplateFileName
        {
            get { return _templateFileName; }
            set
            {
                if (_templateFileName != value)
                {
                    _templateFileName = value;
                    RaisePropertyChanged(() => TemplateFileName);
                }
            }
        }

        private double _scale = 0.2;
        public double Scale
        {
            get { return _scale; }
            set
            {
                if (_scale != value)
                {
                    _scale = value;
                    RaisePropertyChanged(() => Scale);
                }
            }
        }

        private double _perspectiveScale = 0.1;
        public double PerspectiveScale
        {
            get { return _perspectiveScale; }
            set
            {
                if (_perspectiveScale != value)
                {
                    _perspectiveScale = value;
                    RaisePropertyChanged(() => PerspectiveScale);
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

        public void CalculateIsEverythingSelected()
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
        /// Creates a new DrawingDocument,
        /// using the template specified by <paramref name="TemplateFileName"/>.
        /// </summary>
        /// <returns>The new Inventor.DrawingDocument.</returns>
        protected DrawingDocument CreateDrawingDocument()
        {
            var drawingDocument =
                AddIn.CreateDrawingDocument(
                    templateFileName: TemplateFileName,
                    isVisible: true
                );

            drawingDocument.StylesManager.ActiveStandardStyle.ActiveObjectDefaults.LinearDimensionStyle.LinearPrecision = LinearPrecisionEnum.kZeroFractionalLinearPrecision;
            return drawingDocument;
        }
    }
}
