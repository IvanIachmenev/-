using DepartamentIMCS.Models;
using DepartamentIMCS.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;

using Newtonsoft;
using Newtonsoft.Json;

using Xamarin.Forms;

namespace DepartamentIMCS.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Item _selectedItem;
        public string id;

        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command<Item> ItemTapped { get; }

        public ItemsViewModel()
        {
            Title = "Направления";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Item>(OnItemSelected);
        }

        private static readonly string uriSchools = "http://159.223.87.40/schools";
        private static readonly HttpClient client = new HttpClient();
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                Dictionary<string, string> dict = new Dictionary<string, string> { };
                FormUrlEncodedContent form = new FormUrlEncodedContent(dict);
                HttpResponseMessage response = await client.PostAsync(uriSchools, form);
                string result = await response.Content.ReadAsStringAsync();
                List<Dictionary<string, string>> jsonItems = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(result);
                
                foreach (var item in jsonItems)
                {
                    Items.Add(new Item { Id=item["id"], Text=item["name"], Category=item["level_name"], Description=item["discription"]});
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}