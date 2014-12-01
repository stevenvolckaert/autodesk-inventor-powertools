using System;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("PowerTools")]
[assembly: AssemblyDescription("A collection of power tools for Autodesk Inventor 2012.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Steven Volckaert")]
[assembly: AssemblyProduct("PowerTools")]
[assembly: AssemblyCopyright("Copyright © 2014 Steven Volckaert")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(true)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid(StevenVolckaert.InventorPowerTools.AssemblyInfo.Guid)]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: CLSCompliant(false)]
[assembly: NeutralResourcesLanguageAttribute("en-US")]

namespace StevenVolckaert.InventorPowerTools
{
    static internal class AssemblyInfo
    {
        /// <summary>
        /// The application's GUID.
        /// </summary>
        internal const string Guid = "e94e94b4-2808-4450-81ba-07ea75811e27";
    }
}
