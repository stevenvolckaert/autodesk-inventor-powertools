namespace StevenVolckaert
{
    /// <summary>
    /// Provides access to rules that are used for validation.
    /// </summary>
    public static class ValidationRules
    {
        /// <summary>
        /// A regular expression pattern used to validate ISO 3166-1 alpha-2 country codes.
        /// </summary>
        public const string CountryCodeRegexValidationPattern = @"^[A-Z]{2}$";

        /// <summary>
        /// A regular expression pattern used to validate culture names as they are used by System.Globalization.CultureInfo.
        /// </summary>
        public const string CultureNameRegexValidationPattern = @"[a-z]{2}(-[A-Za-z]{2,8})?";

        /// <summary>
        /// A regular expression pattern used to validate email addresses.
        /// </summary>
        public const string EmailAddressRegexValidationPattern = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        /// <summary>
        /// A regular expression pattern used to validate strings which represent a 32-bit signed integer.
        /// </summary>
        public const string Int32RegexValidationPattern = @"^(-)?[0-9]+$";
    }
}
