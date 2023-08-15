using System;
using System.Globalization;
using System.Windows.Data;

namespace UI.WPF.MVVM.ViewModel
{
    public class BooksReadConverter : IValueConverter
    {
        private int StartNumberOfBooksRead { get; set; } = new Random().Next(250, 800);
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var num = StartNumberOfBooksRead;
            StartNumberOfBooksRead -= new Random().Next(1, 20);
            return $"Books read: {num.ToString()}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}