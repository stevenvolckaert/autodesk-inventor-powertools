namespace StevenVolckaert.InventorPowerTools
{
    using System;
    using Inventor;

    public class Assembly : ModelBase, IDocument
    {
        public AssemblyDocument Document { get; }

        public static Func<AssemblyDocument, Assembly> AsAssembly { get; } =
            assemblyDocument => new Assembly(assemblyDocument) { IsSelected = true };

        public string Name
        {
            get { return Document.DisplayName; }
        }

        public string FileName
        {
            get { return Document.DisplayName; }
        }

        public Assembly(AssemblyDocument assemblyDocument)
        {
            if (assemblyDocument == null)
                throw new ArgumentNullException(nameof(assemblyDocument));

            Document = assemblyDocument;
            Document.SetCustomPropertyFormat("Lengte", CustomPropertyPrecisionEnum.kZeroDecimalPlacePrecision, showUnit: false);
            Document.SetCustomPropertyFormat("Breedte", CustomPropertyPrecisionEnum.kZeroDecimalPlacePrecision, showUnit: false);
            Document.SetCustomPropertyFormat("Dikte", CustomPropertyPrecisionEnum.kZeroDecimalPlacePrecision, showUnit: false);
        }
    }
}
