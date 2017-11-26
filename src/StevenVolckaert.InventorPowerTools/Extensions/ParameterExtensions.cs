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
        /// Sets the parameter's custom property format to text, no units, zero decimal place precision, and no
        /// trailing zeros.
        /// </summary>
        /// <param name="parameter">The <see cref="Parameter"/> instance that this extension method affects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parameter"/> is <c>null</c>.</exception>
        public static void SetCustomPropertyFormat(this Parameter parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));

            SetCustomPropertyFormat(
                parameter,
                displayPrecision: CustomPropertyPrecisionEnum.kZeroDecimalPlacePrecision,
                showUnit: false,
                showTrailingZeros: false
            );
        }

        /// <summary>
        /// Sets the format of a custom property.
        /// </summary>
        /// <param name="parameter">The <see cref="Parameter"/> instance that this extension method affects.</param>
        /// <param name="displayPrecision">The precision used to format the parameter's value.</param>
        /// <param name="showUnit">A value which indicates whether to display the parameter's unit.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parameter"/> is <c>null</c>.</exception>
        public static void SetCustomPropertyFormat(
            this Parameter parameter,
            CustomPropertyPrecisionEnum displayPrecision,
            bool showUnit,
            bool showTrailingZeros)
        {
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));

            parameter.CustomPropertyFormat.PropertyType = CustomPropertyTypeEnum.kTextPropertyType;
            parameter.CustomPropertyFormat.Precision = displayPrecision;
            parameter.CustomPropertyFormat.ShowUnitsString = showUnit;
            parameter.CustomPropertyFormat.ShowTrailingZeros = showTrailingZeros;
        }
    }
}
