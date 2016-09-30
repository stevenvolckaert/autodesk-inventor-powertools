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

        public Part(PartDocument part)
        {
            if (part == null)
                throw new ArgumentNullException(nameof(part));

            Document = part;
            Document.SetCustomPropertyFormat("Lengte", CustomPropertyPrecisionEnum.kZeroDecimalPlacePrecision, showUnit: false);
            Document.SetCustomPropertyFormat("Breedte", CustomPropertyPrecisionEnum.kZeroDecimalPlacePrecision, showUnit: false);
            Document.SetCustomPropertyFormat("Dikte", CustomPropertyPrecisionEnum.kZeroDecimalPlacePrecision, showUnit: false);
        }
    }
}
