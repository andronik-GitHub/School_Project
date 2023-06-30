using Application.Features.AuthorFeatures.Commands.UpdateAuthor;
using Application.Features.GenreFeatures.Commands.UpdateGenre;
using Application.Features.GenreFeatures.Queries.Common;
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
using UI.WPF.MVVM.VM.GenreVM;

namespace UI.WPF.MVVM.V.GenreV
{
    /// <summary>
    /// Логика взаимодействия для Edit_Genre.xaml
    /// </summary>
    public partial class Edit_Genre : Window
    {
        private Genre_VM viewModel { get; set; } = new();
        private GenreDTO entity { get; set; }

        public Edit_Genre(GenreDTO obj)
        {
            InitializeComponent();
            entity = obj;
            DataContext = entity;
        }


        private async void Update_Click(object sender, RoutedEventArgs e)
        {
            var updateObj = new UpdateGenreCommand
            {
                Id = entity.GenreId,
                Name = entity.Name
            };
            var response = await viewModel.UpdateAsync(updateObj);
            if (response) this.Close();
        }
    }
}
