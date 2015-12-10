using Microsoft.Practices.Prism.PubSubEvents;

namespace StevenVolckaert.Windows.Mvvm
{
    /// <summary>
    /// An event which indicates that an application preference has changed.
    /// <para>This class cannot be inherited.</para>
    /// </summary>
    public sealed class PreferenceChangedEvent : PubSubEvent<PreferenceType>
    {
    }   
}
