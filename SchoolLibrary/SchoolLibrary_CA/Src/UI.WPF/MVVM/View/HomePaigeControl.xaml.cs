using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UI.WPF.MVVM.View
{
    public partial class HomePaigeControl : UserControl
    {
        public HomePaigeControl()
        {
            InitializeComponent();
        }
        
        private void HeaderBorder_Hover(object sender, RoutedEventArgs args)
        {
            var _border = (Border)sender;

            _border.Background = _border.Background == null ? 
                new SolidColorBrush(Color.FromRgb(51, 51, 51)) : null;
        }
    }
}