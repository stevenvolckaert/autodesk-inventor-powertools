using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace StevenVolckaert.Globalization
{
    /// <summary>
    /// Manages cultures of an application.
    /// </summary>
    public class CultureManager
    {
        /* TODO Write unit tests that verifies correct execution of the methods in this class.
         * For instance, calling any of the five (5) SetCulture methods should return the name of the culture
         * after execution, which must equal the CurrentCultureName property of the culture manager (after execution).
         * Steven Volckaert. January 30, 2013.
         */

        private string _currentCultureName;
        /// <summary>
        /// Gets or sets the name of the current culture in the format "&lt;languagecode2&gt;-&lt;country/regioncode2&gt;".
        /// </summary>
        /// <returns>
        /// The current culture name in the format "&lt;languagecode2&gt;-&lt;country/regioncode2&gt;",
        /// where "&lt;languagecode2&gt;" is a lowercase two-letter code derived from ISO 639-1
        /// and "&lt;country/regioncode2&gt;" is an uppercase two-letter code derived from ISO 3166.
        ///</returns>
        public string CurrentCultureName
        {
            get { return _currentCultureName; }
            set
            {
                if (value == _currentCultureName || String.IsNullOrWhiteSpace(value))
                    return;

                SetCulture(value);
            }
        }

        /// <summary>
        /// Gets the manager's current culture.
        /// </summary>
        public CultureInfo CurrentCulture
        {
            get { return SupportedCultures[CurrentCultureName]; }
        }

        private readonly string _defaultCultureName;
        /// <summary>
        /// Gets the name of the manager's default culture.
        /// </summary>
        public string DefaultCultureName
        {
            get { return _defaultCultureName; }
        }

        private readonly Dictionary<string, CultureInfo> _supportedCultures;
        /// <summary>
        /// Gets a dictionary of cultures that are supported by this culture manager.
        /// </summary>
        public Dictionary<string, CultureInfo> SupportedCultures
        {
            get { return _supportedCultures; }
        }

        /// <summary>
        /// Occurs when the manager's culture has changed.
        /// </summary>
        public event EventHandler<CurrentCultureChangedEventArgs> CurrentCultureChanged;

        /// <summary>
        /// Raises the <see cref="CurrentCultureChanged"/> event.
        /// </summary>
        /// <param name="oldValue">The name of the current culture before the change.</param>
        /// <param name="newValue">The name of the current culture after the change.</param>
        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Justification = "Method used to raise an event.")]
        private void RaiseCurrentCultureChanged(string oldValue, string newValue)
        {
            if (CurrentCultureChanged != null)
                CurrentCultureChanged(this, new CurrentCultureChangedEventArgs(oldValue, newValue));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Barco.CineCare.ClientSupport.Core.CultureManager"/> class.
        /// </summary>
        /// <param name="supportedCultureNames">A collection of culture names that are supported by the manager.
        /// Culture names that are not supported are ignored.</param>
        /// <exception cref="ArgumentNullException"><paramref name="supportedCultureNames"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="supportedCultureNames"/> contains no elements,
        /// or contains no supported culture names.</exception>
        /// <remarks>
        /// The manager's default culture is set to the first item of the <paramref name="supportedCultureNames"/> parameter.
        /// </remarks>
        public CultureManager(IEnumerable<string> supportedCultureNames)
        {
            if (supportedCultureNames == null)
                throw new ArgumentNullException("supportedCultureNames");

            if (supportedCultureNames.Count() == 0)
                throw new ArgumentException("The supportedCultureNames parameter contains no elements.");

            _supportedCultures = new Dictionary<string, CultureInfo>();

            foreach (var cultureName in supportedCultureNames)
            {
                var cultureInfo = GetCultureInfo(cultureName);

                if (cultureInfo == null)
                    continue;

                _supportedCultures.Add(cultureName, cultureInfo);
            }

            if (_supportedCultures.Count() == 0)
                throw new ArgumentException("The supportedCultureNames parameter contains no supported culture names.");

            _defaultCultureName = _supportedCultures.First().Key;
            _currentCultureName = SetDefaultCulture();

            _supportedCultures = _supportedCultures.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Barco.CineCare.ClientSupport.Core.CultureManager"/> class.
        /// </summary>
        /// <param name="supportedCultureNames">A collection of culture names that are supported by the manager.</param>
        /// <param name="defaultCultureName">The name of the manager's default culture.</param>
        /// <exception cref="ArgumentNullException"><paramref name="supportedCultureNames"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="supportedCultureNames"/> contains no elements.</exception>
        /// <remarks>
        /// If the <paramref name="defaultCultureName"/> parameter is not supported, the manager's default culture is set
        /// to the first item of the <paramref name="supportedCultureNames"/> parameter.
        /// </remarks>
        public CultureManager(IEnumerable<string> supportedCultureNames, string defaultCultureName)
            : this(supportedCultureNames)
        {
            if (IsSpecificCultureSupported(defaultCultureName))
            {
                _defaultCultureName = defaultCultureName;
                _currentCultureName = SetDefaultCulture();
            }
        }

        /// <summary>
        /// Returns an instance of the <see cref="System.Globalization.CultureInfo"/> class
        /// based on the culture specified by name, or <c>null</c> if the culture is not
        /// supported by the current operating system.
        /// </summary>
        /// <param name="cultureName">The name of a culture.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="cultureName"/> is <c>null</c>.</exception>
        public static CultureInfo GetCultureInfo(string cultureName)
        {
            try
            {
                return new CultureInfo(cultureName);
            }
            catch (ArgumentNullException)
            {
                /* TODO Apply the correct format to this debug message.
                 * Steven Volckaert. January 26, 2013.
                 */

                /*
                Debug.WriteLine(string.Format(
                    "[{0}] {1}.{2}: Encountered an exception of type '{3}': Culture '{4}' is not supported.",
                    DateTime.Now.ToLongTimeString(),
                    currentMethod.DeclaringType.FullName,
                    currentMethod.Name,
                    typeof(CultureNotFoundException),
                    cultureName
                ));
                */

                Debug.WriteLine("Exception in Barco.Globalization.CultureManager: The parameter cultureName is null.");
                return null;
            }
            catch (CultureNotFoundException)
            {
                /* TODO Apply the correct format to this debug message.
                 * Steven Volckaert. January 26, 2013.
                 */

                Debug.WriteLine("Exception in Barco.Globalization.CultureManager: Culture name '{0}' is not supported.", cultureName);
                return null;
            }
        }

        /// <summary>
        /// Convert a given culture name to the name of it's associated neutral culture.
        /// </summary>
        /// <param name="cultureName">The name of the culture, representing a specific or neutral culture.</param>
        /// <returns>The name of the neutral culture, or <c>null</c> if the culture is not supported.</returns>
        public static string GetNeutralCultureName(string cultureName)
        {
            if (String.IsNullOrWhiteSpace(cultureName))
                return null;

            var cultureInfo = GetCultureInfo(cultureName);

            if (cultureInfo == null)
                return null;

            if (!cultureInfo.IsNeutralCulture)
                cultureInfo = cultureInfo.Parent;

            return cultureInfo.Name;
        }

        /// <summary>
        /// Returns a value that indicates whether a given culture, or its associated neutral culture, is supported by this culture manager.
        /// </summary>
        /// <param name="cultureName">The name of the culture, representing a specific or neutral culture.</param>
        public bool IsCultureSupported(string cultureName)
        {
            if (IsSpecificCultureSupported(cultureName))
                return true;

            cultureName = GetNeutralCultureName(cultureName);

            if (cultureName == null)
                return false;

            return IsSpecificCultureSupported(cultureName);
        }

        /// <summary>
        /// Returns a value that indicates whether a given specific culture is supported by this culture manager.
        /// </summary>
        /// <param name="cultureName">The name of the culture.</param>
        /// <returns><c>true</c> if the culture is supported, <c>false</c> otherwise.</returns>
        public bool IsSpecificCultureSupported(string cultureName)
        {
            if (String.IsNullOrWhiteSpace(cultureName))
                return false;

            return _supportedCultures.ContainsKey(cultureName);
        }

        /// <summary>
        /// Returns a value that indicates whether a given culture, or it's associated neutral culture,
        /// is currently selected.
        /// </summary>
        /// <param name="cultureName">The name of the culture.</param>
        public bool IsCultureSelected(string cultureName)
        {
            return CurrentCultureName.StartsWith(cultureName, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Sets the application's current culture, given a collection of culture names.
        /// The manager selects the first culture that is supported, or the default culture
        /// if none of the given cultures are supported.
        /// </summary>
        /// <param name="cultureNames">A collection of culture names.</param>
        /// <returns>The name of the manager's culture when the operation finishes.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="cultureNames"/> is <c>null</c>.</exception>
        public string SetCulture(IEnumerable<string> cultureNames)
        {
            if (cultureNames == null)
                throw new ArgumentNullException("cultureNames");

            foreach (var cultureName in cultureNames)
                if (IsCultureSupported(cultureName))
                    return SetCulture(cultureName);

            return SetDefaultCulture();
        }

        /// <summary>
        /// Sets the application's current culture.
        /// If the given culture is not supported, the manager's default culture is selected.
        /// <para>
        /// If a given specific culture is not supported, its associated neutral culture is selected (if it exists).
        /// </para>
        /// </summary>
        /// <param name="cultureName">The name of the culture, representing a specific or neutral culture.</param>
        /// <returns>The name of the manager's culture when the operation finishes.</returns>
        /// <exception cref="ArgumentException"><paramref name="cultureName"/> is <c>null</c>, empty, or white space.</exception>
        public string SetCulture(string cultureName)
        {
            if (String.IsNullOrWhiteSpace(cultureName))
                throw new ArgumentException("The cultureName parameter is null, empty, or white space.", "cultureName");

            if (!IsSpecificCultureSupported(cultureName))
                cultureName = GetNeutralCultureName(cultureName);

            return SetSpecificCulture(cultureName);
        }

        /// <summary>
        /// Sets the application's current culture, given an array of culture names.
        /// The manager selects the first culture that is supported, or the default culture
        /// if none of the given cultures are supported.
        /// </summary>
        /// <param name="cultureNames">An array of culture names.</param>
        /// <returns>The name of the manager's culture when the operation finishes.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="cultureNames"/> is <c>null</c>.</exception>
        public string SetCulture(string[] cultureNames)
        {
            if (cultureNames == null)
                throw new ArgumentNullException("cultureNames");

            return SetCulture(new List<string>(cultureNames));
        }

        /// <summary>
        /// Sets the application's current culture to the manager's default culture.
        /// </summary>
        /// <returns>The name of the manager's culture when the operation finishes.</returns>
        public string SetDefaultCulture()
        {
            return SetSpecificCulture(DefaultCultureName);
        }

        /// <summary>
        /// Sets the application's current culture to a specific culture.
        /// If the given culture is not supported, the manager's default culture is selected.
        /// </summary>
        /// <param name="cultureName">The name of the culture.</param>
        /// <returns>The name of the manager's culture when the operation finishes.</returns>
        public string SetSpecificCulture(string cultureName)
        {
            var newCultureName = cultureName;
            var oldCultureName = CurrentCultureName;

            if (!IsSpecificCultureSupported(newCultureName))
                newCultureName = DefaultCultureName;

            Thread.CurrentThread.CurrentCulture = _supportedCultures[newCultureName];
            Thread.CurrentThread.CurrentUICulture = _supportedCultures[newCultureName];

            _currentCultureName = newCultureName;

            RaiseCurrentCultureChanged(oldCultureName, newCultureName);
            return newCultureName;
        }
    }
}
