using System;
using System.Globalization;
using System.Windows.Data;

namespace StevenVolckaert.Windows.Data
{
    /// <summary>
    /// Specifies an operation for text case conversion.
    /// </summary>
    public enum CaseConvertOperation
    {
        /// <summary>
        /// Don't convert case.
        /// </summary>
        None = 0,

        /// <summary>
        /// Convert to uppercase.
        /// </summary>
        ToUpper,

        /// <summary>
        ///  Convert to lowercase.
        /// </summary>
        ToLower,
    }

    /// <summary>
    /// Converts a string to uppercase or lowercase, specified by a <see cref="CaseConvertOperation"/>.
    /// </summary>
    public class CaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return value;

            switch ((CaseConvertOperation)parameter)
            {
                case CaseConvertOperation.None:
                default:
                    return value;
                case CaseConvertOperation.ToLower:
                    return value.ToString().ToLower(culture);
                case CaseConvertOperation.ToUpper:
                    return value.ToString().ToUpper(culture);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
