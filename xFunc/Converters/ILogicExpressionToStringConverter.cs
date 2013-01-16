using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using xFunc.Logics.Expressions;

namespace xFunc.Converters
{

    [ValueConversion(typeof(ILogicExpression), typeof(string))]
    public class ILogicExpressionToStringConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return new ArgumentNullException();

            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

}
