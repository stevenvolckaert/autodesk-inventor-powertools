namespace StevenVolckaert.InventorPowerTools
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Inventor;

    /// <summary>
    ///     Represents the style of views in drawings.
    /// </summary>
    public class DrawingViewStyle
    {
        /// <summary>
        ///     Gets the underlying enumeration value.
        /// </summary>
        public DrawingViewStyleEnum EnumValue { get; }

        /// <summary>
        ///     Gets the display name.
        /// </summary>
        public string DisplayName { get; }

        private static readonly IDictionary<DrawingViewStyleEnum, string> _displayNames =
            new Dictionary<DrawingViewStyleEnum, string>
            {
                { DrawingViewStyleEnum.kFromBaseDrawingViewStyle, "None (Inherit From Base View)" },
                { DrawingViewStyleEnum.kHiddenLineDrawingViewStyle, "Hidden Line" },
                { DrawingViewStyleEnum.kHiddenLineRemovedDrawingViewStyle, "Hidden Line Removed" },
                { DrawingViewStyleEnum.kShadedDrawingViewStyle, "Shaded, Hidden Line Removed" },
                { DrawingViewStyleEnum.kShadedHiddenLineDrawingViewStyle, "Shaded, Hidden Line" }
            };

        /// <summary>
        ///     Initializes a new instance of the <see cref="LinearPrecision"/> class.
        /// </summary>
        /// <param name="enumValue">
        ///     The underlying enumeration value.
        /// </param>
        public DrawingViewStyle(DrawingViewStyleEnum enumValue)
            : this(enumValue, displayName: _displayNames[enumValue])
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="LinearPrecision"/> class.
        /// </summary>
        /// <param name="enumValue">
        ///     The underlying enumeration value.
        /// </param>
        /// <param name="displayName">
        ///     The display name associated with <paramref name="enumValue"/>.
        /// </param>
        public DrawingViewStyle(DrawingViewStyleEnum enumValue, string displayName)
        {
            EnumValue = enumValue;
            DisplayName = displayName;
        }

        public static IEnumerable<DrawingViewStyle> GetSupportedViewStyles()
        {
            return from x in (DrawingViewStyleEnum[])Enum.GetValues(typeof(DrawingViewStyleEnum))
                   select new DrawingViewStyle(enumValue: x, displayName: _displayNames[x]);
        }
    }
}
