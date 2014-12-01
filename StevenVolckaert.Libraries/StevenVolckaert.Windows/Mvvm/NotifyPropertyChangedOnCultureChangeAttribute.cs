using System;

namespace StevenVolckaert.Windows.Mvvm
{
    /// <summary>
    /// Notifies clients that the property's value has changed when the application's culture changes.
    /// <para>This class cannot be inherited.</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public sealed class NotifyPropertyChangedOnCultureChangeAttribute : Attribute
    {
    }
}
