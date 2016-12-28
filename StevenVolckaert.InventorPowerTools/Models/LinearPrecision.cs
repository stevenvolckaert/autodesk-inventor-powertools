namespace StevenVolckaert.InventorPowerTools
{
    using System.Collections.Generic;
    using Inventor;
    using System.Linq;

    /// <summary>
    /// Represents the precision of a linear dimension.
    /// </summary>
    public class LinearPrecision
    {
        /// <summary>
        /// Gets the underlying enumeration value of the linear precision.
        /// </summary>
        public LinearPrecisionEnum EnumValue { get; }

        /// <summary>
        /// Gets the display name of the linear precision.
        /// </summary>
        public string DisplayName { get; }

        // TODO Learn how the enumeration values that represent decimal places are represented in Inventor, and adapt the below string
        // literals, if necessary. What's declared currently is just a guess. Steven Volckaert. September 28, 2016.
        private static readonly IDictionary<LinearPrecisionEnum, string> _decimalDisplayNames =
            new Dictionary<LinearPrecisionEnum, string>
            {
                { LinearPrecisionEnum.kZeroDecimalPlaceLinearPrecision, "0" },
                { LinearPrecisionEnum.kOneDecimalPlaceLinearPrecision, "00" },
                { LinearPrecisionEnum.kTwoDecimalPlacesLinearPrecision, "000" },
                { LinearPrecisionEnum.kThreeDecimalPlacesLinearPrecision, "000" },
                { LinearPrecisionEnum.kFourDecimalPlacesLinearPrecision, "0000" },
                { LinearPrecisionEnum.kFiveDecimalPlacesLinearPrecision, "00000" },
                { LinearPrecisionEnum.kSixDecimalPlacesLinearPrecision, "000000" },
                { LinearPrecisionEnum.kSevenDecimalPlacesLinearPrecision, "0000000" },
                { LinearPrecisionEnum.kEightDecimalPlacesLinearPrecision, "00000000" },

            };

        private static readonly IDictionary<LinearPrecisionEnum, string> _fractionalDisplayNames =
            new Dictionary<LinearPrecisionEnum, string>
            {
                { LinearPrecisionEnum.kZeroFractionalLinearPrecision, "0" },
                { LinearPrecisionEnum.kHalfFractionalLinearPrecision, "1/2" },
                { LinearPrecisionEnum.kQuarterFractionalLinearPrecision, "1/4" },
                { LinearPrecisionEnum.kEightDecimalPlacesLinearPrecision, "1/8" },
                { LinearPrecisionEnum.kSixteenthsFractionalLinearPrecision, "1/16" },
                { LinearPrecisionEnum.kThirtySecondsFractionalLinearPrecision, "1/32" },
                { LinearPrecisionEnum.kSixtyFourthsFractionalLinearPrecision, "1/64" },
                { LinearPrecisionEnum.kOneTwentyEighthsFractionalLinearPrecision, "1/128" }
            };

        /// <summary>
        /// Initializes a new instance of the <see cref="LinearPrecision"/> class.
        /// </summary>
        /// <param name="enumValue">The underlying enumeration value.</param>
        public LinearPrecision(LinearPrecisionEnum enumValue)
            : this(enumValue, GetDisplayName(enumValue))
        {
        }

        private LinearPrecision(LinearPrecisionEnum value, string displayName)
        {
            EnumValue = value;
            DisplayName = displayName;
        }

        /// <summary>
        /// Returns the display name of the specified <see cref="LinearPrecisionEnum"/> value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The display name associated with <paramref name="value"/>.</returns>
        public static string GetDisplayName(LinearPrecisionEnum value)
        {
            string returnValue;
            _decimalDisplayNames.TryGetValue(value, out returnValue);
            return returnValue ?? _fractionalDisplayNames[value];
        }

        /// <summary>
        /// Creates an enumerable of <see cref="LinearPrecision"/> instances that represent the decimal part of a linear precision. 
        /// </summary>
        /// <returns>An enumerable of <see cref="LinearPrecision"/> instances. </returns>
        public static IEnumerable<LinearPrecision> CreateDecimalLinearPrecisions()
        {
            return CreateLinearPrecisions(_decimalDisplayNames);
        }

        /// <summary>
        /// Creates an enumerable of <see cref="LinearPrecision"/> instances that represent the fractional part of a linear precision.
        /// </summary>
        /// <returns>An enumerable of <see cref="LinearPrecision"/> instances. </returns>
        public static IEnumerable<LinearPrecision> CreateFractionalLinearPrecisions()
        {
            return CreateLinearPrecisions(_fractionalDisplayNames);
        }

        private static IEnumerable<LinearPrecision> CreateLinearPrecisions(IDictionary<LinearPrecisionEnum, string> displayNames)
        {
            return displayNames.Select(x => new LinearPrecision(x.Key, x.Value));
        }
    }
}
