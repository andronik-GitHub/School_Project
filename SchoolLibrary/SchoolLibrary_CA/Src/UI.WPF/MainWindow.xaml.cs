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
using UI.WPF.MVVM.V.AuthorV;
using UI.WPF.MVVM.V.BookV;
using UI.WPF.MVVM.V.GenreV;

namespace UI.WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += Author_Block;
        }

        private void Author_Block(object sender, RoutedEventArgs e) => 
            MainWindowContent.Content = new AuthorControl();
        private void Genre_Block(object sender, RoutedEventArgs e) =>
            MainWindowContent.Content = new GenreControl();
        private void Book_Block(object sender, RoutedEventArgs e) =>
            MainWindowContent.Content = new BookControl();
    }
}