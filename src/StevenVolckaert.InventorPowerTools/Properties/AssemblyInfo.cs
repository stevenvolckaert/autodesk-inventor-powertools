using System;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("Steven Volckaert's Inventor® Power Tools")]
[assembly: AssemblyDescription("A collection of productivity tools for Autodesk Inventor® 2012.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Steven Volckaert")]
[assembly: AssemblyProduct(StevenVolckaert.InventorPowerTools.AssemblyInfo.Name)]
[assembly: AssemblyCopyright("Copyright © 2016, 2017 Steven Volckaert")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

[assembly: ComVisible(true)]
[assembly: Guid(StevenVolckaert.InventorPowerTools.AssemblyInfo.Guid)]

[assembly: AssemblyVersion("1.2.1.0")]
[assembly: AssemblyFileVersion("1.2.1.0")]
[assembly: CLSCompliant(false)]
[assembly: NeutralResourcesLanguage("en-US")]

namespace StevenVolckaert.InventorPowerTools
{
    internal static class AssemblyInfo
    {
        /// <summary>
        /// The application's GUID.
        /// </summary>
        internal const string Guid = "6f046c68-7899-4486-9e15-3752a437a94a";

        /// <summary>
        /// The application's name.
        /// </summary>
        internal const string Name = "Inventor Power Tools";
    }
}
