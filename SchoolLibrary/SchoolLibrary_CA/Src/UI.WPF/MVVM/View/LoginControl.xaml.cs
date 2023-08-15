using System.Windows.Controls;
using System.Windows.Input;

namespace UI.WPF.MVVM.View
{
    public partial class LoginControl : UserControl
    {
        public LoginControl()
        {
            InitializeComponent();
        }

        private void BackToHomePaige(object sendes, MouseButtonEventArgs e)
        {
            ContentLoginControl.Content = new MainWindowControl();
        }
    }
}