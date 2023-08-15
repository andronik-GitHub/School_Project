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

namespace UI.WPF.Temp.View.ViewAuthor
{
    /// <summary>
    /// Логика взаимодействия для AuthorControl.xaml
    /// </summary>
    public partial class AuthorControl : UserControl
    {
        HttpClient Client = new();
        public AuthorControl()
        {
            Client.BaseAddress = new Uri("http://localhost:5000/ca/");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            InitializeComponent();
        }

        public async void Start()
        {
            var response = await Client.GetAsync("author");
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                var temp = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<ObservableCollection<AuthorDTO>>(temp);

                itemsList.ItemsSource = list;
                Count.Text = $"Count: {list?.Count()}";
            }
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            //var item = (DTOAnimalVisitor)itemsList.SelectedItem;
            //new animal_visitors_update(item).Show();
        }
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            //new animal_visitors_insert().ShowDialog();
            //Start();
        }
        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            //var item = (DTOAnimalVisitor)itemsList.SelectedItem;
            //if (item is DTOAnimalVisitor)
            //{
            //    var response = await viewModel.DeleteAsync(item.AnimalId, item.VisitorId);
            //    if (response) Start();
            //}
        }
    }
}
