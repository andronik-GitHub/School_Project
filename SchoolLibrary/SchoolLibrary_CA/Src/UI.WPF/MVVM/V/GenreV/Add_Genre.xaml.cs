using Application.Features.AuthorFeatures.Commands.CreateAuthor;
using Application.Features.GenreFeatures.Commands.CreateGenre;
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
    /// Логика взаимодействия для Add_Genre.xaml
    /// </summary>
    public partial class Add_Genre : Window
    {
        private Genre_VM viewModel { get; set; } = new();

        public Add_Genre()
        {
            InitializeComponent();
        }


        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            var entity = new CreateGenreCommand
            {
                Name = GenreName.Text
            };
            var response = await viewModel.InsertAsync(entity);
            if (response != Guid.Empty) this.Close();
        }
    }
}
