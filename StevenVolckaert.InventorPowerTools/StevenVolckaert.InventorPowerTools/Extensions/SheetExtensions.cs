using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Inventor;

namespace StevenVolckaert.InventorPowerTools
{
    /// <summary>
    /// Provides extension methods for Inventor.Sheet objects.
    /// </summary>
    public static class SheetExtensions
    {
        public static PartsList AddPartsList(this Sheet sheet, object viewOrModel, PartsListLevelEnum level)
        {
            if (sheet == null)
                throw new ArgumentNullException("sheet");

            if (viewOrModel == null)
                throw new ArgumentNullException("viewOrModel");

            var partsList =
                sheet.PartsLists.Add(
                    ViewOrModel: viewOrModel,
                    PlacementPoint: sheet.TopRightCorner(),
                    Level: level,
                    NumberingScheme: Type.Missing,
                    NumberOfSections: 1,
                    WrapLeft: true
                );

            partsList.ShowTitle = false;

            //MessageBox.Show(GetColumnIds(partsList));

            partsList.RemoveColumns(
                new Dictionary<string, int>
                {
                    { "{32853F0F-3444-11D1-9E93-0060B03C1CA6}", 5 }, // "PART NUMBER"
                    //{ "{32853F0F-3444-11D1-9E93-0060B03C1CA6}", 29 } // "DESCRIPTION"
                },
                PropertyTypeEnum.kItemPartsListProperty, PropertyTypeEnum.kQuantityPartsListProperty
            );

            partsList.AddCustomPropertyColumn("Lengte");
            partsList.AddCustomPropertyColumn("Breedte");
            partsList.AddCustomPropertyColumn("Dikte");

            partsList.SetColumnValuesHorizontalJustification(HorizontalTextAlignmentEnum.kAlignTextCenter);

            return partsList;
        }

        private static string GetColumnIds(PartsList partsList)
        {
            var stringBuilder = new StringBuilder();

            foreach (PartsListColumn column in partsList.PartsListColumns)
            {
                if (column.PropertyType == PropertyTypeEnum.kFileProperty)
                {
                    var filePropertyId = column.GetFilePropertyId();

                    stringBuilder.Append(
                        string.Format(
                            CultureInfo.InvariantCulture,
                            "Title = '{0}', PropertyType = '{1}', FilePropertyId = <'{2}', '{3}'>\r\n",
                            column.Title, column.PropertyType, filePropertyId.Key, filePropertyId.Value
                        ));
                }
                else
                    stringBuilder.Append(
                        string.Format(
                            CultureInfo.InvariantCulture,
                            "Title = '{0}', PropertyType = '{1}'\r\n",
                            column.Title, column.PropertyType
                        )
                    );
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Returns the sheet's center point.
        /// </summary>
        /// <param name="sheet">The Inventor.Sheet object that this extension method affects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="sheet"/> is <c>null</c>.</exception>
        public static Point2d CenterPoint(this Sheet sheet)
        {
            if (sheet == null)
                throw new ArgumentNullException("sheet");

            return AddIn.CreatePoint2D(sheet.Width / 2, sheet.Height / 2);
        }

        /// <summary>
        /// Returns the margin of the sheet's border.
        /// </summary>
        /// <param name="sheet">The Inventor.Sheet object that this extension method affects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="sheet"/> is <c>null</c>.</exception>
        public static Margin Margin(this Sheet sheet)
        {
            if (sheet == null)
                throw new ArgumentNullException("sheet");

            return sheet.Border == null
                ? new Margin()
                : new Margin
                  {
                      Left = sheet.Border.RangeBox.MinPoint.X,
                      Top = sheet.Height - sheet.Border.RangeBox.MaxPoint.Y,
                      Right = sheet.Width - sheet.Border.RangeBox.MaxPoint.X,
                      Bottom = sheet.Border.RangeBox.MinPoint.Y
                  };
        }

        /// <summary>
        /// Returns a point in the top right corner, useful for placing small views that add an overview.
        /// </summary>
        /// <param name="sheet">The Inventor.Sheet object that this extension method affects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="sheet"/> is <c>null</c>.</exception>
        public static Point2d TopRightPoint(this Sheet sheet)
        {
            if (sheet == null)
                throw new ArgumentNullException("sheet");

            var margin = sheet.Margin();

            return
                AddIn.CreatePoint2D(
                    ((sheet.Width - margin.Right) * 7 + margin.Left) / 8,
                    ((sheet.Height - margin.Top) * 7 + margin.Bottom) / 8
                );
        }

        public static Point2d TopRightPoint(this Sheet sheet, double horizontalOffset, double verticalOffset)
        {
            if (sheet == null)
                throw new ArgumentNullException("sheet");

            var margin = sheet.Margin();

            return
                AddIn.CreatePoint2D(
                    ((sheet.Width - margin.Right) * 7 + margin.Left) / 8 + horizontalOffset,
                    ((sheet.Height - margin.Top) * 7 + margin.Bottom) / 8 + verticalOffset
                );
        }

        /// <summary>
        /// Returns the sheet's top right corner.
        /// </summary>
        /// <param name="sheet">The Inventor.Sheet object that this extension method affects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="sheet"/> is <c>null</c>.</exception>
        public static Point2d TopRightCorner(this Sheet sheet)
        {
            return sheet.Border == null
                ? AddIn.CreatePoint2D(sheet.Width, sheet.Height)
                : sheet.Border.RangeBox.MaxPoint;
        }
    }
}
