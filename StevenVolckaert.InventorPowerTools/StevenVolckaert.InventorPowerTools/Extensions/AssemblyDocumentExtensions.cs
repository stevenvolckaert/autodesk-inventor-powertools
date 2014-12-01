using System;
using System.Collections.Generic;
using System.Linq;
using Inventor;

namespace StevenVolckaert.InventorPowerTools
{
    public static class AssemblyDocumentExtensions
    {
        /// <summary>
        /// Sets the format of a custom property.
        /// </summary>
        /// <param name="assembly">The Inventor.AssemblyDocument instance that this extension method affects.</param>
        /// <param name="propertyName">The name of the custom property.</param>
        /// <param name="displayPrecision">The value's display precision.</param>
        /// <param name="showUnit">A value which indicates whether to display the value's unit.</param>
        /// <exception cref="ArgumentNullException"><paramref name="assembly"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="propertyName"/> is <c>null</c> or empty.</exception>
        public static void SetCustomPropertyFormat(this AssemblyDocument assembly, string propertyName, CustomPropertyPrecisionEnum displayPrecision, bool showUnit)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            // NOTE uncommenting the next argument check always throws an exception. Probably a bug in the Inventor API.

            //if (string.IsNullOrEmpty(propertyName))
            //    throw new ArgumentException("Argument is null or empty.", "propertyName");

            try
            {
                assembly.ComponentDefinition.Parameters[propertyName].SetCustomPropertyFormat(displayPrecision, showUnit);
            }
            catch
            {
            }
        }

        public static List<Assembly> Subassemblies(this AssemblyDocument assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            return
                assembly.AllReferencedDocuments
                .OfType<AssemblyDocument>().Reverse()
                .Select(Assembly.AsAssembly)
                .ToList();
        }

        /// <summary>
        /// Returns all parts.
        /// </summary>
        /// <param name="assembly">The Inventor.AssemblyDocument instance that this extension method affects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="assembly"/> is <c>null</c>.</exception>
        public static List<PartDocument> Parts(this AssemblyDocument assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            return assembly.AllReferencedDocuments.OfType<PartDocument>().Reverse().ToList();
        }

        /// <summary>
        /// Returns all generic parts.
        /// </summary>
        /// <param name="assembly">The Inventor.AssemblyDocument instance that this extension method affects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="assembly"/> is <c>null</c>.</exception>
        public static List<PartDocument> GenericParts(this AssemblyDocument assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            return assembly.Parts().Where(x => x.IsGenericPart()).ToList();
        }

        /// <summary>
        /// Returns all sheet metal parts.
        /// </summary>
        /// <param name="assembly">The Inventor.AssemblyDocument instance that this extension method affects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="assembly"/> is <c>null</c>.</exception>
        public static List<Part> SheetMetalParts(this AssemblyDocument assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            return assembly.Parts().Where(x => x.IsSheetMetal()).Select(Part.AsPart).ToList();
        }

        /// <summary>
        /// Returns the unit quantity of a specified part.
        /// </summary>
        /// <param name="assemblyDocument">The Inventor.AssemblyDocument instance that this extension method affects.</param>
        /// <param name="part">The part.</param>
        /// <exception cref="ArgumentNullException"><paramref name="assembly"/> or <paramref name="part"/>is <c>null</c>.</exception>
        public static int GetPartQuantity(this AssemblyDocument assembly, PartDocument part)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            if (part == null)
                throw new ArgumentNullException("part");

            var bom = assembly.ComponentDefinition.BOM;
            bom.PartsOnlyViewEnabled = true;

            foreach (BOMRow row in bom.BOMViews["Parts Only"].BOMRows)
            {
                var document = (Document)row.ComponentDefinitions[1].Document;

                if (document.FullFileName == part.FullFileName)
                    return row.ItemQuantity;
            }

            return 0;
        }

        //public static Dictionary<string, List<string>> PartFileNameMap(this AssemblyDocument assembly)
        //{
        //    if (assembly == null)
        //        throw new ArgumentNullException("assembly");

        //    var map =
        //        new Dictionary<string, List<string>>
        //        {
        //            { assembly.FullFileName, new List<string>() }
        //        };

        //    GetPartFileNameMap(assembly, map);

        //    return map;
        //}

        //private static void GetPartFileNameMap(AssemblyDocument assembly, Dictionary<string, List<string>> map)
        //{
        //    foreach (ComponentOccurrence occurrence in assembly.ComponentDefinition.Occurrences)
        //    {
        //        var document = (Document)occurrence.Definition.Document;
        //        var fullFileName = document.FullFileName;
        //        var subassembly = document as AssemblyDocument;

        //        if (subassembly == null)
        //            map[assembly.FullFileName].Add(fullFileName);
        //        else
        //        {
        //            if (map.ContainsKey(fullFileName))
        //                continue;

        //            map.Add(fullFileName, new List<string>());
        //            GetPartFileNameMap(subassembly, map);
        //        }
        //    }
        //}

        /// <summary>
        /// Returns all MDF parts.
        /// </summary>
        /// <param name="assembly">The Inventor.AssemblyDocument instance that this extension method affects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="assembly"/> is <c>null</c>.</exception>
        public static List<PartDocument> MdfParts(this AssemblyDocument assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            return assembly.Parts().Where(x => x.IsMdf()).ToList();
        }

        //private int CountAssemblies(AssemblyDocument assemblyDocument)
        //{
        //    var dict = new Dictionary<string, int>();
        //    string msg = string.Empty;

        //    RecurseSubassembly(assemblyDocument, dict);

        //    foreach (var keyValuePair in dict)
        //        msg += keyValuePair.Key + ": " + keyValuePair.Value.ToString();

        //    ShowMessageBox(msg);

        //    return 0;
        //}

        //private void RecurseSubassembly(AssemblyDocument assemblyDocument, Dictionary<string, int> dict)
        //{
        //    foreach (var doc in assemblyDocument.ReferencedDocuments.OfType<AssemblyDocument>())
        //    {
        //        var path = doc.FullFileName;

        //        if (dict.ContainsKey(path))
        //            dict[path]++;
        //        else
        //            dict.Add(path, value: 1);

        //        RecurseSubassembly(doc, dict);
        //    }
        //}
    }
}
