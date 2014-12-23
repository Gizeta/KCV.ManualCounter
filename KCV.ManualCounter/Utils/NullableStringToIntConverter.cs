using System;
using System.Globalization;
using System.Windows.Data;

namespace Gizeta.KCV.ManualCounter.Utils
{
    public class NullableStringToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int val = (int)value;
            return val == 0 && parameter as string == bool.TrueString ? string.Empty : val.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int retVal;
            return int.TryParse(value.ToString(), out retVal) ? retVal : 0;
        }
    }
}
