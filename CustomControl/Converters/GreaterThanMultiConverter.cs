using System;
using System.Globalization;
using System.Windows.Data;

namespace CustomControl.Converters
{
    public class GreaterThanMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values is not null &&
                values.Length == 2 &&
                double.TryParse(values[0].ToString(), out var number1) &&
                double.TryParse(values[1].ToString(), out var number2) ?
                number1 > number2 :
                false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
