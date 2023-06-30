using System.Windows;
using UI.WPF.Temp.View;

namespace UI.WPF.MVVM.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowContent.Content = new tempControl();
        }
    }
}