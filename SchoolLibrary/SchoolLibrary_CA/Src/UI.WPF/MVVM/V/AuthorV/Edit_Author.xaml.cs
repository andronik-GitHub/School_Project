using Application.Features.AuthorFeatures.Commands.UpdateAuthor;
using Application.Features.AuthorFeatures.Queries.Common;
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
    /// Логика взаимодействия для Edit_Author.xaml
    /// </summary>
    public partial class Edit_Author : Window
    {
        private Author_VM viewModel { get; set; } = new();
        private AuthorDTO entity { get; set; }

        public Edit_Author(AuthorDTO obj)
        {
            InitializeComponent();
            entity = obj;
            DataContext = entity;
        }


        private async void Update_Click(object sender, RoutedEventArgs e)
        {
            var updateObj = new UpdateAuthorCommand
            {
                Id = entity.AuthorId,
                FirstName = entity.FirstName, 
                LastName = entity.LastName,
                Birthdate = entity.Birthdate,
                Nationality = entity.Nationality,
            };
            var response = await viewModel.UpdateAsync(updateObj);
            if (response) this.Close();
        }
    }
}
