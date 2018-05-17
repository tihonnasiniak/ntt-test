using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ntt_test.Converters
{
    class LongitudeToYConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value + 180;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}