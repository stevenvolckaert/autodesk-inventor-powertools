namespace StevenVolckaert.InventorPowerTools
{
    using System;
    using Inventor;

    public class Assembly : ModelBase, IDocument
    {
        private readonly AssemblyDocument _assemblyDocument;
        public AssemblyDocument Document
        {
            get { return _assemblyDocument; }
        }

        public static readonly Func<AssemblyDocument, Assembly> AsAssembly =
            assemblyDocument => new Assembly(assemblyDocument) { IsSelected = true };

        public string Name
        {
            get { return _assemblyDocument.DisplayName; }
        }

        public string FileName
        {
            get { return _assemblyDocument.DisplayName; }
        }

        public Assembly(AssemblyDocument assemblyDocument)
        {
            if (assemblyDocument == null)
                throw new ArgumentNullException(nameof(assemblyDocument));

            _assemblyDocument = assemblyDocument;
            _assemblyDocument.SetCustomPropertyFormat("Lengte", CustomPropertyPrecisionEnum.kZeroDecimalPlacePrecision, showUnit: false);
            _assemblyDocument.SetCustomPropertyFormat("Breedte", CustomPropertyPrecisionEnum.kZeroDecimalPlacePrecision, showUnit: false);
            _assemblyDocument.SetCustomPropertyFormat("Dikte", CustomPropertyPrecisionEnum.kZeroDecimalPlacePrecision, showUnit: false);
        }
    }
}
