using System;

namespace StevenVolckaert.Windows.Mvvm
{
    /// <summary>
    /// Notifies clients that the property's value changed when the application's system of measurement changes.
    /// <para>This class cannot be inherited.</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public sealed class NotifyPropertyChangedOnSystemOfMeasurementChangeAttribute : Attribute
    {
    }
}
