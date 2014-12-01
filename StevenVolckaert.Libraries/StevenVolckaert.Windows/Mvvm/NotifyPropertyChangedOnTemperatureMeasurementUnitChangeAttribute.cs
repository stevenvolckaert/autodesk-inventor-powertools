using System;

namespace StevenVolckaert.Windows.Mvvm
{
    // TODO See http://msdn.microsoft.com/en-us/library/26etazsy(v=vs.110).aspx

    /// <summary>
    /// Notifies clients that the property's value changed when the application's temperature measurement unit changes.
    /// This class cannot be inherited.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public sealed class NotifyPropertyChangedOnTemperatureMeasurementUnitChangeAttribute : Attribute
    {
    }
}
