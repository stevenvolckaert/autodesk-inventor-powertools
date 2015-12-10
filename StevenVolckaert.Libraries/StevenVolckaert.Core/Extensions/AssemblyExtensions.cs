using System;
using System.Reflection;

namespace StevenVolckaert
{
    /// <summary>
    /// Provides extension methods for System.Reflection.Assembly objects.
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Gets the name of the assembly.
        /// </summary>
        /// <param name="assembly">The assembly this extension method affects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="assembly"/> is <c>null</c>.</exception>
        public static string Name(this Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            return assembly.FullName.Split(',')[0];
        }
    }
}
