namespace StevenVolckaert.InventorPowerTools
{
    using System;
    using Inventor;

    /// <summary>
    /// Provides extension methods for <see cref="Parameter"/> instances.
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
                throw new ArgumentNullException(nameof(parameter));

            parameter.CustomPropertyFormat.PropertyType = CustomPropertyTypeEnum.kTextPropertyType;
            parameter.CustomPropertyFormat.ShowUnitsString = showUnit;
            parameter.CustomPropertyFormat.Precision = displayPrecision;
            //parameter.CustomPropertyFormat.ShowTrailingZeros
        }
    }
}
