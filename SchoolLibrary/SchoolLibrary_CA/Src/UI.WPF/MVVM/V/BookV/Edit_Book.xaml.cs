using Application.Features.BookFeatures.Commands.UpdateBook;
using Application.Features.BookFeatures.Queries.Common;
using Application.Features.GenreFeatures.Commands.UpdateGenre;
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
    /// Логика взаимодействия для Edit_Book.xaml
    /// </summary>
    public partial class Edit_Book : Window
    {
        private Book_VM viewModel { get; set; } = new();
        private Publisher_VM publisherViewModel { get; set; } = new();
        private BookDTO entity { get; set; }
        private PublisherDTO? Publisher { get; set; }

        public Edit_Book(BookDTO obj)
        {
            InitializeComponent();
            OnStartup();
            entity = obj;
            DataContext = entity;
        }


        private async void OnStartup()
        {
            var list = await publisherViewModel.GetListAsync();
            Publisher = list?.FirstOrDefault(x => x.PublisherId == entity.PublisherId);
            publisherList.ItemsSource = list;
        }
        private async void Update_Click(object sender, RoutedEventArgs e)
        {
            var publisher = (PublisherDTO)publisherList.SelectedItem;
            if (publisher != null) entity.PublisherId = publisher.PublisherId;

            var updateObj = new UpdateBookCommand
            {
                Id = entity.BookId,
                Title = entity.Title,
                PublishingYear = entity.PublishingYear,
                ISBN = entity.ISBN,
                PublisherId = publisher?.PublisherId ?? entity.PublisherId,
            };
            var response = await viewModel.UpdateAsync(updateObj);
            if (response) this.Close();
        }
    }
}
