using System;

namespace StevenVolckaert.Globalization
{
    /// <summary>
    /// Provides data for CurrentCultureChanged subscribers.
    /// </summary>
    public class CurrentCultureChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the name of the current culture after the change.
        /// </summary>
        public string NewValue { get; private set; }

        /// <summary>
        /// Gets the name of the current culture before the change.
        /// </summary>
        public string OldValue { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Barco.CineCare.ClientSupport.CurrentCultureChangedEventArgs"/> class.
        /// </summary>
        /// <param name="oldValue">The name of the current culture before the change.</param>
        /// <param name="newValue">The name of the current culture after the change.</param>
        public CurrentCultureChangedEventArgs(string oldValue, string newValue)
        {
            NewValue = newValue;
            OldValue = oldValue;
        }
    }
}
