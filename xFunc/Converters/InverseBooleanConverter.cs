using System;
using System.Globalization;
using System.Windows.Data;

namespace xFunc.Converters
{

    [ValueConversion(typeof(bool), typeof(bool))]
    public class InverseBooleanConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(bool))
                return !(bool)value;

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(bool))
                return !(bool)value;

            return null;
        }

    }

}
