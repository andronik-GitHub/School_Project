using Application.Features.BookFeatures.Commands.CreateBook;
using Application.Features.GenreFeatures.Commands.CreateGenre;
using Application.Features.PublisherFeatures.Queries.Common;
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
using UI.WPF.MVVM.VM.BookVM;
using UI.WPF.MVVM.VM.GenreVM;
using UI.WPF.MVVM.VM.PublisherVM;

namespace UI.WPF.MVVM.V.BookV
{
    /// <summary>
    /// Логика взаимодействия для Add_Book.xaml
    /// </summary>
    public partial class Add_Book : Window
    {
        private Book_VM viewModel { get; set; } = new();
        private Publisher_VM publisherViewModel { get; set; } = new();

        public Add_Book()
        {
            InitializeComponent();
            OnStartup();
        }


        private async void OnStartup()
        {
            var list = await publisherViewModel.GetListAsync();
            publisherList.ItemsSource = list;
        }
        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            var publisher = (PublisherDTO)publisherList.SelectedItem;
            var entity = new CreateBookCommand
            {
                Title = Title.Text,
                PublishingYear = Convert.ToInt32(PublishingYear.Text),
                ISBN = ISBN.Text,
                PublisherId = publisher.PublisherId,
            };
            var response = await viewModel.InsertAsync(entity);
            if (response != Guid.Empty) this.Close();
        }
    }
}
