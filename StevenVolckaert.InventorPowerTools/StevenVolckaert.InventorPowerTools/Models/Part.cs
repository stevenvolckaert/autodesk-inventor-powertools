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
            SetCustomPropertyFormat(CustomPropertyPrecisionEnum.kZeroDecimalPlacePrecision, showTrailingZeros: false);
        }

        public void SetCustomPropertyFormat(CustomPropertyPrecisionEnum displayPrecision, bool showTrailingZeros)
        {
            foreach (var propertyName in CustomPropertyNames)
            {
                var parameter = Document.ComponentDefinition.Parameters.TryGetValue(propertyName);

                try
                {
                    parameter?.SetCustomPropertyFormat(
                        displayPrecision,
                        showUnit: false,
                        showTrailingZeros: showTrailingZeros
                    );
                }
                catch
                {
                }
            }
        }

        public void SetCustomPropertyFormat(LinearPrecision linearPrecision, bool showTrailingZeros)
        {
            if (linearPrecision == null)
                throw new ArgumentNullException(nameof(linearPrecision));

            SetCustomPropertyFormat(
                ConvertToCustomPropertyPrecisionEnum(linearPrecision.EnumValue),
                showTrailingZeros
            );
        }
    }
}
