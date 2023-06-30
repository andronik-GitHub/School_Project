using Application.Features.AuthorFeatures.Commands.CreateAuthor;
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
using System.Windows.Shapes;
using UI.WPF.MVVM.VM.AuthorVM;

namespace UI.WPF.MVVM.V.AuthorV
{
    /// <summary>
    /// Логика взаимодействия для Add_Author.xaml
    /// </summary>
    public partial class Add_Author : Window
    {
        private Author_VM viewModel { get; set; } = new();

        public Add_Author()
        {
            InitializeComponent();
        }


        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            var entity = new CreateAuthorCommand
            {
                FirstName = FirstName.Text,
                LastName = LastName.Text,
                Birthdate = Birthdate.SelectedDate ?? DateTime.UtcNow,
                Nationality = Nationality.Text
            };
            var response = await viewModel.InsertAsync(entity);
            if (response != Guid.Empty) this.Close();
        }
    }
}
