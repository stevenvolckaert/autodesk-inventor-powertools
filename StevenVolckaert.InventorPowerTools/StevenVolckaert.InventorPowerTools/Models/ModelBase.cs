namespace StevenVolckaert.InventorPowerTools
{
    using Inventor;
    using Microsoft.Practices.Prism.ViewModel;
    using System.Collections.Generic;

    public abstract class ModelBase : NotificationObject
    {
        protected static _Document ActiveDocument
        {
            get { return AddIn.Inventor.ActiveDocument; }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    RaisePropertyChanged(() => IsSelected);
                }
            }
        }

        /// <summary>
        /// Returns the active drawing document, or <c>null</c> if the active document is not a
        /// drawing document, or no document is active.
        /// </summary>
        /// <returns>The active <see cref="DrawingDocument"/> instance, or <c>null</c> if there is none.</returns>
        protected DrawingDocument TryGetActiveDrawingDocument()
        {
            return ActiveDocument as DrawingDocument;
        }

        private static Dictionary<LinearPrecisionEnum, CustomPropertyPrecisionEnum> _linearPrecisionEnumMapping =
            new Dictionary<LinearPrecisionEnum, CustomPropertyPrecisionEnum>
            {
                { LinearPrecisionEnum.kZeroDecimalPlaceLinearPrecision, CustomPropertyPrecisionEnum.kZeroDecimalPlacePrecision },
                { LinearPrecisionEnum.kOneDecimalPlaceLinearPrecision, CustomPropertyPrecisionEnum.kOneDecimalPlacePrecision },
                { LinearPrecisionEnum.kTwoDecimalPlacesLinearPrecision, CustomPropertyPrecisionEnum.kTwoDecimalPlacesPrecision },
                { LinearPrecisionEnum.kThreeDecimalPlacesLinearPrecision, CustomPropertyPrecisionEnum.kThreeDecimalPlacesPrecision },
                { LinearPrecisionEnum.kFourDecimalPlacesLinearPrecision, CustomPropertyPrecisionEnum.kFourDecimalPlacesPrecision },
                { LinearPrecisionEnum.kFiveDecimalPlacesLinearPrecision, CustomPropertyPrecisionEnum.kFiveDecimalPlacesPrecision },
                { LinearPrecisionEnum.kSixDecimalPlacesLinearPrecision, CustomPropertyPrecisionEnum.kSixDecimalPlacesPrecision },
                { LinearPrecisionEnum.kSevenDecimalPlacesLinearPrecision, CustomPropertyPrecisionEnum.kSevenDecimalPlacesPrecision },
                { LinearPrecisionEnum.kEightDecimalPlacesLinearPrecision, CustomPropertyPrecisionEnum.kEightDecimalPlacesPrecision },
                { LinearPrecisionEnum.kHalfFractionalLinearPrecision, CustomPropertyPrecisionEnum.kHalfFractionalLengthPrecision },
                { LinearPrecisionEnum.kQuarterFractionalLinearPrecision, CustomPropertyPrecisionEnum.kQuarterFractionalLengthPrecision },
                { LinearPrecisionEnum.kEightDecimalPlacesLinearPrecision, CustomPropertyPrecisionEnum.kEightDecimalPlacesPrecision },
                { LinearPrecisionEnum.kSixteenthsFractionalLinearPrecision, CustomPropertyPrecisionEnum.kSixteenthsFractionalLengthPrecision },
                { LinearPrecisionEnum.kThirtySecondsFractionalLinearPrecision, CustomPropertyPrecisionEnum.kThirtySecondsFractionalLengthPrecision },
                { LinearPrecisionEnum.kSixtyFourthsFractionalLinearPrecision, CustomPropertyPrecisionEnum.kSixteenthsFractionalLengthPrecision },
                { LinearPrecisionEnum.kOneTwentyEighthsFractionalLinearPrecision, CustomPropertyPrecisionEnum.kOneTwentyEighthsFractionalLengthPrecision }
            };

        public static CustomPropertyPrecisionEnum ConvertToCustomPropertyPrecisionEnum(LinearPrecisionEnum linearPrecisionEnum)
        {
            return _linearPrecisionEnumMapping[linearPrecisionEnum];
        }
    }
}
