using System;
using System.Globalization;
using System.Windows.Data;

namespace CustomControl.Converters
{
    public class DivisionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return double.TryParse(value.ToString(), out var numerator) &&
                double.TryParse(parameter.ToString(), out var denominator) &&
                denominator != 0 ?
                numerator / denominator :
                0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
