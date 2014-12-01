using System;
using Inventor;

namespace StevenVolckaert.InventorPowerTools
{
    public class Part : ModelBase, IDocument
    {
        private readonly PartDocument _part;
        public PartDocument Document
        {
            get { return _part; }
        }

        public static readonly Func<PartDocument, Part> AsPart =
            partDocument => new Part(partDocument) { IsSelected = true };

        public string Name
        {
            get { return _part.DisplayName; }
        }

        public string FileName
        {
            get { return _part.DisplayName; }
        }

        public Part(PartDocument part)
        {
            if (part == null)
                throw new ArgumentNullException("part");

            _part = part;
            _part.SetCustomPropertyFormat("Lengte", CustomPropertyPrecisionEnum.kZeroDecimalPlacePrecision, showUnit: false);
            _part.SetCustomPropertyFormat("Breedte", CustomPropertyPrecisionEnum.kZeroDecimalPlacePrecision, showUnit: false);
            _part.SetCustomPropertyFormat("Dikte", CustomPropertyPrecisionEnum.kZeroDecimalPlacePrecision, showUnit: false);
        }
    }
}
