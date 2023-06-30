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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UI.WPF.MVVM.V.AuthorV;
using UI.WPF.MVVM.VM.GenreVM;

namespace UI.WPF.MVVM.V.GenreV
{
    /// <summary>
    /// Логика взаимодействия для GenreControl.xaml
    /// </summary>
    public partial class GenreControl : UserControl
    {
        Genre_VM viewModel = new();

        public GenreControl()
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
            var item = (GenreDTO)itemsList.SelectedItem;
            new Edit_Genre(item).Show();
        }
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            new Add_Genre().ShowDialog();
            OnStartup();
        }
        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            var item = (GenreDTO)itemsList.SelectedItem;
            if (item is GenreDTO)
            {
                var response = await viewModel.DeleteAsync(item.GenreId);
                if (response) OnStartup();
            }
        }
    }
}
