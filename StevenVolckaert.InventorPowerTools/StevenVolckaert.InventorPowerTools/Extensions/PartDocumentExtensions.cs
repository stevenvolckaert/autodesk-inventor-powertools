namespace StevenVolckaert.InventorPowerTools
{
    using System;
    using Inventor;

    /// <summary>
    /// Provides extension methods for <see cref="PartDocument"/> instances.
    /// </summary>
    public static class PartDocumentExtensions
    {
        // See Documentation/DocCLSIDs.h for the GUIDs that identify a document's SubType.

        private const string PartDocumentId = "{4D29B490-49B2-11D0-93C3-7E0706000000}";
        private const string PartDocumentName = "Autodesk Inventor Part";
        private const string SheetMetalPartDocumentId = "{9C464203-9BAE-11D3-8BAD-0060B0CE6BB4}";
        private const string SheetMetalPartDocumentName = "Autodesk Inventor Sheet Metal Part";
        private const string GenericProxyPartDocumentId = "{92055419-B3FA-11D3-A479-00C04F6B9531}";
        private const string GenericProxyPartDocumentName = "Autodesk Inventor Generic Proxy Part";
        private const string CompatibilityProxyPartDocumentId = "{9C464204-9BAE-11D3-8BAD-0060B0CE6BB4}";
        private const string CompatibilityProxyPartDocumentName = "Autodesk Inventor Compatibility Proxy Part";
        private const string CatalogProxyPartDocumentId = "{9C88D3AF-C3EB-11D3-B79E-0060B0F159EF}";
        private const string CatalogProxyPartDocumentName = "Autodesk Inventor Catalog Proxy Part";
        private const string MoldedPartDocumentId = "{4D8D80D4-F5B0-4460-8CEA-4CD222684469}";
        private const string MoldedPartDocumentName = "Autodesk Inventor Molded Part Document";

        /// <summary>
        /// Returns a value that indicates whether the document is an Autodesk Inventor Part.
        /// </summary>
        /// <param name="partDocument">
        /// The <see cref="PartDocument"/> instance that this extension method affects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="partDocument"/> is <c>null</c>.</exception>
        public static bool IsGenericPart(this PartDocument partDocument)
        {
            if (partDocument == null)
                throw new ArgumentNullException(nameof(partDocument));

            return partDocument.SubType.Equals(PartDocumentId);
        }

        /// <summary>
        /// Returns a value that indicates whether the document is a Autodesk Inventor Sheet Metal Part.
        /// </summary>
        /// <param name="partDocument">
        /// The <see cref="PartDocument"/> instance that this extension method affects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="partDocument"/> is <c>null</c>.</exception>
        public static bool IsSheetMetal(this PartDocument partDocument)
        {
            if (partDocument == null)
                throw new ArgumentNullException(nameof(partDocument));

            return partDocument.SubType.Equals(SheetMetalPartDocumentId);
        }

        /// <summary>
        /// Returns a value that indicates wheter the document is an Autodesk Inventor Part and MDF.
        /// </summary>
        /// <param name="partDocument">
        /// The <see cref="PartDocument"/> instance that this extension method affects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="partDocument"/> is <c>null</c>.</exception>
        public static bool IsMdf(this PartDocument partDocument)
        {
            if (partDocument == null)
                throw new ArgumentNullException(nameof(partDocument));

            return partDocument.IsGenericPart() && partDocument.FullFileName.ToUpperInvariant().Contains("MDF");
        }

        public static string SubTypeString(this PartDocument partDocument)
        {
            if (partDocument == null)
                throw new ArgumentNullException(nameof(partDocument));

            var subType = partDocument.SubType;

            switch (subType)
            {
                case PartDocumentId:
                    return PartDocumentName;
                case SheetMetalPartDocumentId:
                    return SheetMetalPartDocumentName;
                case GenericProxyPartDocumentId:
                    return GenericProxyPartDocumentName;
                case CompatibilityProxyPartDocumentId:
                    return CompatibilityProxyPartDocumentName;
                case CatalogProxyPartDocumentId:
                    return CatalogProxyPartDocumentName;
                case MoldedPartDocumentId:
                    return MoldedPartDocumentName;
                default:
                    return subType;
            }
        }

        /// <summary>
        /// Sets the format of a custom property.
        /// </summary>
        /// <param name="partDocument">
        /// The <see cref="PartDocument"/> instance that this extension method affects.</param>
        /// <param name="propertyName">The name of the custom property.</param>
        /// <param name="displayPrecision">The value's display precision.</param>
        /// <param name="showUnit">A value which indicates whether to display the value's unit.</param>
        /// <exception cref="ArgumentNullException"><paramref name="partDocument"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="propertyName"/> is <c>null</c> or empty.</exception>
        public static void SetCustomPropertyFormat(this PartDocument partDocument, string propertyName, CustomPropertyPrecisionEnum displayPrecision, bool showUnit)
        {
            if (partDocument == null)
                throw new ArgumentNullException(nameof(partDocument));

            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Argument is null or empty.", nameof(propertyName));

            try
            {
                var parameter = partDocument.ComponentDefinition.Parameters[propertyName];
                parameter.SetCustomPropertyFormat(displayPrecision, showUnit);
            }
            catch
            {
            }
        }
    }
}
