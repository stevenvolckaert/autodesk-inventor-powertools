namespace StevenVolckaert.InventorPowerTools
{
    using System.Collections.Generic;
    using System.Linq;
    using Inventor;

    /// <summary>
    ///     Represents the precision of a linear dimension.
    /// </summary>
    public class LinearPrecision : Enumeration<LinearPrecisionEnum>
    {
        // TODO Learn how the enumeration values that represent decimal places are represented in Inventor,
        // and adapt the below string literals, if necessary. What's declared currently is just a guess.
        // Steven Volckaert. September 28, 2016.
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

        private LinearPrecision(LinearPrecisionEnum enumValue, string displayName)
            : base(enumValue, displayName)
        {
        }

        /// <summary>
        ///     Gets a list of <see cref="LinearPrecision"/> instances
        ///     that represent the decimal part of a linear precision.
        /// </summary>
        public static IList<LinearPrecision> SupportedDecimalLinearPrecisions { get; } =
            _decimalDisplayNames.Select(x => new LinearPrecision(x.Key, displayName: x.Value)).ToList();

        /// <summary>
        ///     Gets a list of <see cref="LinearPrecision"/> instances
        ///     that represent the fractional part of a linear precision.
        /// </summary>
        public static IList<LinearPrecision> SupportedFractionalLinearPrecisions { get; } =
            _fractionalDisplayNames.Select(x => new LinearPrecision(x.Key, displayName: x.Value)).ToList();
    }
}
