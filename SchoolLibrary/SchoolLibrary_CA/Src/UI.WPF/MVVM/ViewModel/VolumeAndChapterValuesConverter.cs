using System;
using System.Globalization;
using System.Windows.Data;

namespace UI.WPF.MVVM.ViewModel
{
    public class VolumeAndChapterValuesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var volume = new Random().Next(1, 100);
            var chapter = new Random().Next(1, 100);

            if (parameter is string param && param == "Short")
                return $"Vol {volume.ToString()} Chap {chapter.ToString()}";
            
            return $"Volume {volume.ToString()} Chapter {chapter.ToString()}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}