using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace UI.WPF.MVVM.ViewModel
{
    public class DateToNewVisibilityConverter : IValueConverter
    {
        public object Convert(object year, Type targetType, object parameter, CultureInfo culture)
        {
            return (DateTime.Now - new DateTime((int)year, 1, 1)).Days < 7 ? 
                Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}