using System;
using System.Resources;
using System.Windows.Data;
using xFunc.Maths.Expressions;
using xFunc.Resources;

namespace xFunc.Converters
{

    [ValueConversion(typeof(MathParameterType), typeof(string))]
    public class MathParameterTypeToStringConverter : IValueConverter
    {

        ResourceManager rm = new ResourceManager(typeof(Resource));

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return rm.GetString("MathParameterType_" + value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

}
