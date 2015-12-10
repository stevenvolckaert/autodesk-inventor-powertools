using System;
using Inventor;

namespace StevenVolckaert.InventorPowerTools
{
    public class Assembly : ModelBase, IDocument
    {
        private readonly AssemblyDocument _assembly;
        public AssemblyDocument Document
        {
            get { return _assembly; }
        }

        public static readonly Func<AssemblyDocument, Assembly> AsAssembly =
            assemblyDocument => new Assembly(assemblyDocument) { IsSelected = true };

        public string Name
        {
            get { return _assembly.DisplayName; }
        }

        public string FileName
        {
            get { return _assembly.DisplayName; }
        }

        public Assembly(AssemblyDocument assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            _assembly = assembly;
            _assembly.SetCustomPropertyFormat("Lengte", CustomPropertyPrecisionEnum.kZeroDecimalPlacePrecision, showUnit: false);
            _assembly.SetCustomPropertyFormat("Breedte", CustomPropertyPrecisionEnum.kZeroDecimalPlacePrecision, showUnit: false);
            _assembly.SetCustomPropertyFormat("Dikte", CustomPropertyPrecisionEnum.kZeroDecimalPlacePrecision, showUnit: false);
        }
    }
}
