using System;
using Inventor;

namespace StevenVolckaert.InventorPowerTools
{
    /// <summary>
    /// Provides extension methods for Inventor.Parameter objects.
    /// </summary>
    internal static class ParameterExtensions
    {
        /// <summary>
        /// Sets the parameter's CustomPropertyFormat to text, no units, and zero decimal place precision.
        /// </summary>
        /// <param name="parameter"></param>
        public static void SetCustomPropertyFormat(this Parameter parameter)
        {
            SetCustomPropertyFormat(parameter, displayPrecision: CustomPropertyPrecisionEnum.kZeroDecimalPlacePrecision, showUnit: false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="displayPrecision"></param>
        /// <param name="showUnit"></param>
        public static void SetCustomPropertyFormat(this Parameter parameter, CustomPropertyPrecisionEnum displayPrecision, bool showUnit)
        {
            if (parameter == null)
                throw new ArgumentNullException("parameter");

            parameter.CustomPropertyFormat.PropertyType = CustomPropertyTypeEnum.kTextPropertyType;
            parameter.CustomPropertyFormat.ShowUnitsString = showUnit;
            parameter.CustomPropertyFormat.Precision = displayPrecision;
        }
    }
}
