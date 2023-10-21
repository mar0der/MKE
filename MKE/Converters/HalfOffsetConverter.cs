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
                double size = 0.0;
                if (parameter is double p)
                {
                    size = p;
                }
                else if (parameter is string s && double.TryParse(s, out double parsedSize))
                {
                    size = parsedSize;
                }

                return position - (size / 2);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}