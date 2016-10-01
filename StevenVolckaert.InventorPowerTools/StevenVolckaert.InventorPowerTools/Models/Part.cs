namespace StevenVolckaert.InventorPowerTools
{
    using System;
    using Inventor;

    public class Part : ModelBase, IDocument
    {
        public PartDocument Document { get; }

        public static Func<PartDocument, Part> AsPart { get; } =
            partDocument => new Part(partDocument) { IsSelected = true };

        public string Name
        {
            get { return Document.DisplayName; }
        }

        public string FileName
        {
            get { return Document.DisplayName; }
        }

        public Part(PartDocument partDocument)
        {
            if (partDocument == null)
                throw new ArgumentNullException(nameof(partDocument));

            Document = partDocument;

            var dimensionStyle = TryGetActiveDrawingDocument()?.ActiveLinearDimensionStyle();
            var linearPrecision = dimensionStyle?.LinearPrecision ?? LinearPrecisionEnum.kZeroDecimalPlaceLinearPrecision;
            var customPropertyPrecisionEnum = AddIn.AsCustomPropertyPrecisionEnum(linearPrecision);

            Document.SetCustomPropertyFormat("Lengte", customPropertyPrecisionEnum, showUnit: false);
            Document.SetCustomPropertyFormat("Breedte", customPropertyPrecisionEnum, showUnit: false);
            Document.SetCustomPropertyFormat("Dikte", customPropertyPrecisionEnum, showUnit: false);
        }
    }
}
