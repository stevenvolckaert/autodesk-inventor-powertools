using System;

namespace StevenVolckaert.Web.Mvc
{
    /// <summary>
    /// Ensures that the decorated class or method is ignored when executing <see cref="GlobalizationFilterAttribute"/> action filters.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false)]
    public sealed class IgnoreGlobalizationFilterAttribute : Attribute
    {
    }
}
