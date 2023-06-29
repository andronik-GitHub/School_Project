using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Application.Features.BookFeatures.Queries.Common;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UI.WPF.MVVM.ViewModel.Data;

namespace UI.WPF.MVVM.View
{
    public partial class MainWindowControl : UserControl
    {
        private readonly HttpClient _client = new();
        private SchoolLibraryContext _dbContext { get; } = new();
        public MainWindowControl()
        {
            _client.BaseAddress = new Uri("http://localhost:5000/ca/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept
                .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                
            InitializeComponent();
            Load_MainWindwod();
        }

        private async void Load_MainWindwod()
        {
            var bookResponde = await _client.GetAsync("book");
            if (bookResponde.IsSuccessStatusCode)
            {
                var temp = await bookResponde.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<ObservableCollection<BookDTO>>(temp);
                booksList.ItemsSource = list;
                updateBooksList.ItemsSource = list?.Take(6);
            }
            _dbContext.Users.Take(100).Load();
            usersList.ItemsSource = _dbContext.Users.Local.ToObservableCollection().Skip(10).Take(10);
        }
        

        private void HeaderButtonHoverEnter(object sender, MouseEventArgs e)
        {
            var border = (Border)sender;
            border.Background = new SolidColorBrush(Color.FromRgb(51, 51, 53));
        }
        private void HeaderButtonHoverLeave(object sender, MouseEventArgs e)
        {
            var border = (Border)sender;
            border.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void LoginButton(object sendes, MouseButtonEventArgs e)
        {
            MainWindowContent.Content = new LoginControl();
        }
    }
}