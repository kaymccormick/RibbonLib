using System;
using System.Globalization;
using System.Windows.Data;

namespace RibbonLib.Model
{
    public class EmConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
                if (parameter != null)
                    return (double) value * (double) parameter;
            return 0;
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}