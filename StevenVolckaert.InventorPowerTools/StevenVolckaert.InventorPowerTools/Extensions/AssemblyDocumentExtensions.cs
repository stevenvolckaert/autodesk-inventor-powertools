namespace StevenVolckaert.InventorPowerTools
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Inventor;

    /// <summary>
    /// Provides extension methods for <see cref="AssemblyDocument"/> instances.
    /// </summary>
    public static class AssemblyDocumentExtensions
    {
        /// <summary>
        /// Sets the format of a custom property.
        /// </summary>
        /// <param name="assembly">
        /// The <see cref="AssemblyDocument"/> instance that this extension method affects.</param>
        /// <param name="propertyName">The name of the custom property.</param>
        /// <param name="displayPrecision">The value's display precision.</param>
        /// <param name="showUnit">A value which indicates whether to display the value's unit.</param>
        /// <exception cref="ArgumentNullException"><paramref name="assembly"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="propertyName"/> is <c>null</c> or empty.</exception>
        public static void SetCustomPropertyFormat(
            this AssemblyDocument assembly,
            string propertyName,
            CustomPropertyPrecisionEnum displayPrecision,
            bool showUnit
        )
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));

            // NOTE Uncommenting the next argument check always throws an exception.
            // This is likely a bug in the Inventor API.

            //if (string.IsNullOrEmpty(propertyName))
            //    throw new ArgumentException("Argument is null or empty.", "propertyName");

            try
            {
                var parameter = assembly.ComponentDefinition.Parameters[propertyName];
                parameter.SetCustomPropertyFormat(displayPrecision, showUnit);
                

                // TODO Set parameter.DisplayFormat instead of SetCustomPropertyFormat?
            }
            catch
            {
            }
        }

        public static List<Assembly> Subassemblies(this AssemblyDocument assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));

            return
                assembly.AllReferencedDocuments
                .OfType<AssemblyDocument>().Reverse()
                .Select(Assembly.AsAssembly)
                .ToList();
        }

        /// <summary>
        /// Returns all parts.
        /// </summary>
        /// <param name="assembly">
        /// The <see cref="AssemblyDocument"/> instance that this extension method affects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="assembly"/> is <c>null</c>.</exception>
        public static List<PartDocument> Parts(this AssemblyDocument assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));

            return assembly.AllReferencedDocuments.OfType<PartDocument>().Reverse().ToList();
        }

        /// <summary>
        /// Returns all generic parts.
        /// </summary>
        /// <param name="assembly">
        /// The <see cref="AssemblyDocument"/> instance that this extension method affects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="assembly"/> is <c>null</c>.</exception>
        public static List<PartDocument> GenericParts(this AssemblyDocument assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));

            return assembly.Parts().Where(x => x.IsGenericPart()).ToList();
        }

        /// <summary>
        /// Returns all sheet metal parts.
        /// </summary>
        /// <param name="assembly">
        /// The <see cref="AssemblyDocument"/> instance that this extension method affects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="assembly"/> is <c>null</c>.</exception>
        public static List<Part> SheetMetalParts(this AssemblyDocument assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));

            return assembly.Parts().Where(x => x.IsSheetMetal()).Select(Part.AsPart).ToList();
        }

        /// <summary>
        /// Returns the unit quantity of a specified part.
        /// </summary>
        /// <param name="assemblyDocument">
        /// The <see cref="AssemblyDocument"/> instance that this extension method affects.</param>
        /// <param name="part">The part.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="assembly"/> or <paramref name="part"/>is <c>null</c>.</exception>
        public static int GetPartQuantity(this AssemblyDocument assembly, PartDocument part)
        {
            return GetPartQuantity(assembly, part, displayWarnings: false);
        }

        /// <summary>
        /// Returns the unit quantity of a specified part.
        /// </summary>
        /// <param name="assemblyDocument">
        /// The <see cref="AssemblyDocument"/> instance that this extension method affects.</param>
        /// <param name="part">The part.</param>
        /// <param name="displayWarnings">A value which indicates wheter to display warnings, if they occur.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="assembly"/> or <paramref name="part"/>is <c>null</c>.</exception>
        public static int GetPartQuantity(this AssemblyDocument assembly, PartDocument part, bool displayWarnings)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));

            if (part == null)
                throw new ArgumentNullException(nameof(part));

            var bom = assembly.ComponentDefinition.BOM;

            if (bom == null)
                return 0;

            if (displayWarnings && bom.RequiresUpdate)
                AddIn.ShowWarningMessageBox(
                    AssemblyInfo.Name,
                    $"The BOM of assembly '{assembly.DisplayName}' requires an update. " +
                        "Quantities displayed in the generated drawing might be incorrect."
                );

            try
            {
                bom.PartsOnlyViewEnabled = true;
            }
            catch (ArgumentException)
            {
                return 0;
            }

            BOMView partsOnlyView;

            if (bom.BOMViews.TryGetValue("Parts Only", out partsOnlyView))
                foreach (BOMRow row in partsOnlyView.BOMRows)
                {
                    if (row.ComponentDefinitions.Count == 0)
                        continue;

                    var componentDefinition = row.PrimaryComponentDefinition();

                    if (componentDefinition == null)
                        continue;

                    var document = componentDefinition.Document as Document;

                    if (document == null)
                        continue;

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
        /// <param name="assembly">
        /// The <see cref="AssemblyDocument"/> instance that this extension method affects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="assembly"/> is <c>null</c>.</exception>
        public static List<PartDocument> MdfParts(this AssemblyDocument assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));

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
