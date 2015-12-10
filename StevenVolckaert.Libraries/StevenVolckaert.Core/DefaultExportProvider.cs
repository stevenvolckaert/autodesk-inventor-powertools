using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Globalization;

namespace StevenVolckaert
{
    /// <summary>
    /// Provides exports for a .NET application.
    /// </summary>
    public class DefaultExportProvider : ExportProvider
    {
        private Dictionary<string, string> _mappings;

        public DefaultExportProvider()
        {
            /* NOTE Define mappings from interfaces to concrete types in the _mappings field below.
             * Steven Volckaert. April 29, 2013.
             */

            _mappings = new Dictionary<string, string>
            {
                //{ typeof(IClassContract).FullName, typeof(ImplementingClass).FullName },
            };
        }

        protected override IEnumerable<Export> GetExportsCore(ImportDefinition definition, AtomicComposition atomicComposition)
        {
            if (definition == null)
                throw new ArgumentNullException("definition");

            var exports = new List<Export>();
            string implementingType;

            if (_mappings.TryGetValue(definition.ContractName, out implementingType))
            {
                var type = Type.GetType(implementingType);

                if (type == null)
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Type not found for interface '{0}'.", definition.ContractName));

                var instance = type.GetConstructor(Type.EmptyTypes).Invoke(null);
                var exportDefinition = new ExportDefinition(definition.ContractName, new Dictionary<string, object>());
                var export = new Export(exportDefinition, () => instance);

                exports.Add(export);
            }

            return exports;
        }
    }
}
