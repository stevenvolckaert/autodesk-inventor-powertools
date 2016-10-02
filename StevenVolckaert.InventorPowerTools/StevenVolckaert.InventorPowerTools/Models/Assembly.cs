namespace StevenVolckaert.InventorPowerTools
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        /// <summary>
        /// Gets the individual parts contained in the assembly.
        /// </summary>
        public IEnumerable<Part> Parts
        {
            get { return Document.Parts().Select(Part.AsPart); }
        }

        public Assembly(AssemblyDocument assemblyDocument)
        {
            if (assemblyDocument == null)
                throw new ArgumentNullException(nameof(assemblyDocument));

            Document = assemblyDocument;
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
