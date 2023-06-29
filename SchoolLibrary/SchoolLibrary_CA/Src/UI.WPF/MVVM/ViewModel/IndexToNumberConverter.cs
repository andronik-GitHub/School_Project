using System;
using System.Globalization;
using System.Windows.Data;

namespace UI.WPF.MVVM.ViewModel
{
    public class IndexToNumberConverter : IValueConverter
    {
        public int StartNumber { get; set; }
        public int EndNumber { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            StartNumber += 1;
            return "#" + StartNumber;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}