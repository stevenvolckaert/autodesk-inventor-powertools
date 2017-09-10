namespace StevenVolckaert.InventorPowerTools
{
    using System.Collections.Generic;
    using System.Linq;
    using Inventor;

    /// <summary>
    ///     Represents the style of views in drawings.
    /// </summary>
    public class DrawingViewStyle : Enumeration<DrawingViewStyleEnum>
    {
        private static readonly IDictionary<DrawingViewStyleEnum, string> _displayNamesMap =
            new Dictionary<DrawingViewStyleEnum, string>
            {
                { DrawingViewStyleEnum.kFromBaseDrawingViewStyle, "None (Inherit From Base View)" },
                { DrawingViewStyleEnum.kHiddenLineDrawingViewStyle, "Hidden Line" },
                { DrawingViewStyleEnum.kHiddenLineRemovedDrawingViewStyle, "Hidden Line Removed" },
                { DrawingViewStyleEnum.kShadedDrawingViewStyle, "Shaded, Hidden Line Removed" },
                { DrawingViewStyleEnum.kShadedHiddenLineDrawingViewStyle, "Shaded, Hidden Line" }
            };

        private DrawingViewStyle(DrawingViewStyleEnum enumValue, string displayName)
            : base(enumValue, displayName)
        {
        }

        public static IList<DrawingViewStyle> SupportedValues { get; } =
                _displayNamesMap.Select(x => new DrawingViewStyle(x.Key, displayName: x.Value)).ToList();
    }
}
