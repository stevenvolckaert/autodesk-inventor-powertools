using System;
using Microsoft.Practices.Prism.Mvvm;

namespace StevenVolckaert.Windows.Mvvm
{
    /// <summary>
    /// Provides extension methods for Microsoft.Practices.Prism.Mvvm.BindableBase objects.
    /// </summary>
    [CLSCompliant(false)]
    public static class BindableBaseExtensions
    {
        /// <summary>
        /// Notifies listeners that the values of a number of properties has changed.
        /// </summary>
        /// <param name="bindableBase">The BindableBase instance that this extension method affects.</param>
        /// <param name="propertyNames">The names of the properties used to notify listeners.</param>
        /// <exception cref="ArgumentNullException"><paramref name="bindableBase"/> is <c>null</c>.</exception>
        public static void OnPropertyChanged(this BindableBase bindableBase, params string[] propertyNames)
        {
            if (bindableBase == null)
                throw new ArgumentNullException("bindableBase");

            if (propertyNames != null)
                foreach (var propertyName in propertyNames)
                    bindableBase.OnPropertyChanged(propertyName);
        }
    }
}
