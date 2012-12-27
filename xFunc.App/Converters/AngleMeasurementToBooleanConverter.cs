using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using xFunc.Library.Expressions.Maths;

namespace xFunc.App.Converters
{

    [ValueConversion(typeof(AngleMeasurement), typeof(bool))]
    public class AngleMeasurementToBooleanConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            AngleMeasurement val = (AngleMeasurement)value;
            string param = (string)parameter;

            if (param == "degree")
                return val == AngleMeasurement.Degree;
            if (param == "radian")
                return val == AngleMeasurement.Radian;
            if (param == "gradian")
                return val == AngleMeasurement.Gradian;

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

}
