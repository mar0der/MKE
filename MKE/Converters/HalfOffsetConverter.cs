using System;
using System.Globalization;
using System.Windows.Data;

namespace MKE.Converters
{
    public class HalfOffsetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double position)
            {
                return position - 7.5; // Since the rectangle is 100x100, we offset by 50.
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
