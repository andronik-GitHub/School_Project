using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UI.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void HeaderBorder_Hover(object sender, RoutedEventArgs args)
        {
            var temp = (Border)sender;
            if (temp.Background == null)
            {
                temp.Background = new SolidColorBrush(Color.FromRgb(51, 51, 51));
            }
            else
            {
                temp.Background = null;
            }
        }
    }
}