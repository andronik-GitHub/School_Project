using Application.Features.BookFeatures.Queries.Common;
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
using UI.WPF.MVVM.V.GenreV;
using UI.WPF.MVVM.VM.BookVM;
using UI.WPF.MVVM.VM.GenreVM;

namespace UI.WPF.MVVM.V.BookV
{
    /// <summary>
    /// Логика взаимодействия для BookControl.xaml
    /// </summary>
    public partial class BookControl : UserControl
    {
        Book_VM viewModel = new();

        public BookControl()
        {
            InitializeComponent();
            OnStartup();
        }


        public async void OnStartup()
        {
            var list = await viewModel.GetListAsync();
            itemsList.ItemsSource = list;
            Count.Text = $"Count: {list?.Count()}";
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var item = (BookDTO)itemsList.SelectedItem;
            new Edit_Book(item).ShowDialog();
            OnStartup();
        }
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            new Add_Book().ShowDialog();
            OnStartup();
        }
        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            var item = (BookDTO)itemsList.SelectedItem;
            if (item is BookDTO)
            {
                var response = await viewModel.DeleteAsync(item.BookId);
                if (response) OnStartup();
            }
        }
    }
}
