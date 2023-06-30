using Application.Features.AuthorFeatures.Queries.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
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
using UI.WPF.MVVM.VM.AuthorVM;

namespace UI.WPF.MVVM.V.AuthorV
{
    /// <summary>
    /// Логика взаимодействия для AuthorControl.xaml
    /// </summary>
    public partial class AuthorControl : UserControl
    {
        Author_VM viewModel = new();

        public AuthorControl()
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
            var item = (AuthorDTO)itemsList.SelectedItem;
            new Edit_Author(item).Show();
        }
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            new Add_Author().ShowDialog();
            OnStartup();
        }
        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            var item = (AuthorDTO)itemsList.SelectedItem;
            if (item is AuthorDTO)
            {
                var response = await viewModel.DeleteAsync(item.AuthorId);
                if (response) OnStartup();
            }
        }
    }
}
