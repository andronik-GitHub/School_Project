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
using UI.WPF.Temp.View.ViewAuthor;

namespace UI.WPF.Temp.View
{
    /// <summary>
    /// Логика взаимодействия для tempControl.xaml
    /// </summary>
    public partial class tempControl : UserControl
    {
        public tempControl()
        {
            InitializeComponent();
            Loaded += Author_Select;
        }

        private void Author_Select(object sender, RoutedEventArgs e)
        {
            MainWindowContent.Content = new AuthorControl();
        }
        private void Genre_Select(object sender, RoutedEventArgs e)
        {
            MainWindowContent.Content = new tempControl();
        }
    }
}
