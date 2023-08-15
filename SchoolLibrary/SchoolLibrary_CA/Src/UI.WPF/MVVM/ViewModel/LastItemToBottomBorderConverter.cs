using System;
using System.Globalization;
using System.Windows.Data;

namespace UI.WPF.MVVM.ViewModel
{
    public class LastItemToBottomBorderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int itemCount && itemCount > 0)
            {
                int currentIndex = (int)parameter;
                return currentIndex == itemCount - 1 ? 0 : 1;
            }
            return 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}