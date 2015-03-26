using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Data;

namespace StevenVolckaert.Windows.Data
{
    /// <summary>
    /// Base class for value converters that take a System.Object as an input parameter.
    /// </summary>
    /// <typeparam name="TSource">The type of the source that must be converter.</typeparam>
    /// <typeparam name="TTarget">The type of the value to convert to, which must be a value type.</typeparam>
    public abstract class ObjectToValueConverter<TSource, TTarget> : IValueConverter
        where TTarget : struct
    {
        public TTarget FalseValue { get; set; }
        public TTarget TrueValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(TTarget) && targetType != typeof(Nullable<TTarget>))
            {
                var message = String.Format(CultureInfo.InvariantCulture, "Target must be of type '{0}'.", typeof(TTarget));
                Diagnostics.Debug.WriteLine(MethodBase.GetCurrentMethod(), message);
                throw new InvalidOperationException(message);
            }

            var trueValue = TrueValue;
            var falseValue = FalseValue;

            if (parameter != null && String.Equals(parameter.ToString(), "INVERT", StringComparison.OrdinalIgnoreCase))
            {
                trueValue = FalseValue;
                falseValue = TrueValue;
                parameter = null;
            }

            return IsValueValid((TSource)value, parameter) ? trueValue : falseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a value that indicates wheter an object is valid, which determines the value converter's outcome.
        /// </summary>
        /// <param name="value">The value to check.</param>
        protected abstract bool IsValueValid(TSource value, object parameter);
    }

    public class EnumerableToBooleanConverter : ObjectToValueConverter<IEnumerable<Object>, Boolean>
    {
        public EnumerableToBooleanConverter()
        {
            TrueValue = true;
            FalseValue = false;
        }

        protected override bool IsValueValid(IEnumerable<Object> value, object parameter)
        {
            return value == null
                ? false
                : value.Count() > 0;
        }
    }

    //public class EnumerableToVisibilityConverter : ObjectToValueConverter<IEnumerable<Object>, Visibility>
    //{
    //    public EnumerableToVisibilityConverter()
    //    {
    //        TrueValue = Visibility.Visible;
    //        FalseValue = Visibility.Collapsed;
    //    }

    //    protected override bool IsValueValid(IEnumerable<Object> value, object parameter)
    //    {
    //        return value == null
    //            ? false
    //            : value.Count() > 0;
    //    }
    //}

    public class ObjectToVisibilityConverter : ObjectToValueConverter<Object, Visibility>
    {
        public ObjectToVisibilityConverter()
        {
            TrueValue = Visibility.Visible;
            FalseValue = Visibility.Collapsed;
        }

        protected override bool IsValueValid(Object value, object parameter)
        {
            var stringValue = value as String;

            return stringValue == null
                ? value != null
                : !String.IsNullOrEmpty(stringValue);
        }
    }

    public class StringToVisibilityConverter : ObjectToValueConverter<String, Visibility>
    {
        public StringToVisibilityConverter()
        {
            TrueValue = Visibility.Visible;
            FalseValue = Visibility.Collapsed;
        }

        protected override bool IsValueValid(string value, object parameter)
        {
            if (String.IsNullOrWhiteSpace(value))
                return false;

            if (parameter == null)
                return true;

            return String.Equals(value, parameter.ToString(), StringComparison.OrdinalIgnoreCase);
        }
    }
}
