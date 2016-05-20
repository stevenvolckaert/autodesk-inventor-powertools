namespace StevenVolckaert.InventorPowerTools
{
    using System;
    using System.Collections.ObjectModel;
    using System.Globalization;

    public enum PartTypeEnum
    {
        Generic,
        SheetMetal,
        GenericProxy,
        CompatibilityProxy,
        CatalogProxy,
        Molded
    }

    public sealed class PartType
    {
        private static readonly ReadOnlyCollection<PartType> _supportedPartTypes =
            Array.AsReadOnly(
                new PartType[]
                {
                    new PartType(PartTypeEnum.Generic),
                    new PartType(PartTypeEnum.SheetMetal),
                }
            );
        /// <summary>
        /// Gets a collection of part types that are supported by the application.
        /// </summary>
        public static ReadOnlyCollection<PartType> SupportedPartTypes
        {
            get { return _supportedPartTypes; }
        }

        public string Name { get; private set; }
        public string Id { get; private set; }
        public PartTypeEnum Type { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PartType"/> class.
        /// </summary>
        /// <param name="partType">A <see cref="PartTypeEnum"/> value that indicates the part type.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="partType"/> has an illegal value.</exception>
        /// <remarks>See Documentation/DocCLSIDs.h for the GUIDs that identify a document's SubType.</remarks>
        public PartType(PartTypeEnum partType)
        {
            Type = partType;

            switch (partType)
            {
                case PartTypeEnum.Generic:
                    Id = "4D29B490-49B2-11D0-93C3-7E0706000000";
                    Name = "Autodesk Inventor Part";
                    break;
                case PartTypeEnum.SheetMetal:
                    Id = "9C464203-9BAE-11D3-8BAD-0060B0CE6BB4";
                    Name = "Autodesk Inventor Sheet Metal Part";
                    break;
                case PartTypeEnum.GenericProxy:
                    Id = "92055419-B3FA-11D3-A479-00C04F6B9531";
                    Name = "Autodesk Inventor Generic Proxy Part";
                    break;
                case PartTypeEnum.CompatibilityProxy:
                    Id = "9C464204-9BAE-11D3-8BAD-0060B0CE6BB4";
                    Name = "Autodesk Inventor Compatibility Proxy Part";
                    break;
                case PartTypeEnum.CatalogProxy:
                    Id = "{9C88D3AF-C3EB-11D3-B79E-0060B0F159EF}";
                    Name = "Autodesk Inventor Catalog Proxy Part";
                    break;
                case PartTypeEnum.Molded:
                    Id = "4D8D80D4-F5B0-4460-8CEA-4CD222684469";
                    Name = "Autodesk Inventor Molded Part Document";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("partType", string.Format(CultureInfo.InvariantCulture, "Illegal enum value {0}.", partType));
            }
        }

        public override string ToString()
        {
            return Type.ToString();
        }
    }
}
