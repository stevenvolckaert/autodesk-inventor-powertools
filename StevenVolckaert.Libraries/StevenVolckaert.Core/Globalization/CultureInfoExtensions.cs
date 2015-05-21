using System;
using System.Collections.Generic;
using System.Globalization;

namespace StevenVolckaert.Globalization
{
    /// <summary>
    /// Provides extension methods for System.Globalization.CultureInfo objects.
    /// </summary>
    public static class CultureInfoExtensions
    {
        // MARC Code List for Languages
        // http://www.loc.gov/marc/languages/language_code.html

        // TODO Verify dictionary mapping at http://www.loc.gov/marc/languages/language_code.html.
        private static readonly Dictionary<String, String> _cultureNametoMarcLanguageCodeDictionary =
            new Dictionary<String, String>
            {
                { "zh-Hans", "chi"},
                { "zh-CHS", "chi"},
                { "zh-Hant", "cht"},
                { "zh-CHT", "cht"},
            };

        private static readonly Dictionary<String, String> _twoLetterISOLanguageNameToMarcLanguageCodeDictionary =
            new Dictionary<String, String>
            {
                { "ar", "ara" },
                { "nl", "dut" },
                { "en", "eng" },
                { "fr", "fre" },
                { "de", "ger" },
                { "el", "gre" },
                { "he", "heb" },
                { "hi", "ind" },
                { "it", "ita" },
                { "pl", "pol" },
                { "pt", "por" },
                { "ru", "rus" },
                { "es", "spa" },
                { "tr", "tur" },
                { "uk", "ukr" },
                { "ur", "urd" },
                { "vi", "vie" },
                { "zh", "chi" },
            };

        /// <summary>
        /// Gets the MARC three-letter code for the language of the current
        /// System.Globalization.CultureInfo.
        /// </summary>
        /// <param name="cultureInfo">The System.Globalization.CultureInfo instance this extension method affects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="cultureInfo"/> is <c>null</c>.</exception>
        /// <exception cref="NotSupportedException">Translating <paramref name="cultureInfo"/> to
        /// the associated MARC three-letter language code is not supported.</exception>
        /// <remarks>http://en.wikipedia.org/wiki/MARC_standards</remarks>
        public static string MarcLanguageCode(this CultureInfo cultureInfo)
        {
            if (cultureInfo == null)
                throw new ArgumentNullException("cultureInfo");

            string returnValue;

            if (_cultureNametoMarcLanguageCodeDictionary.TryGetValue(cultureInfo.Name, out returnValue))
                return returnValue;

            if (_twoLetterISOLanguageNameToMarcLanguageCodeDictionary.TryGetValue(cultureInfo.TwoLetterISOLanguageName, out returnValue))
                return returnValue;
            
            throw new NotSupportedException(String.Format(CultureInfo.CurrentCulture, Properties.Resources.ValueNotSupported, cultureInfo.Name));
        }
    }
}
