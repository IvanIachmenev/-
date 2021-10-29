using DepartamentIMCS.Views;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;

using Newtonsoft;
using Newtonsoft.Json;

using Xamarin.Forms;
using DepartamentIMCS.Models;

namespace DepartamentIMCS.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        public string userId;
        private string text;
        private string description;
        private string category;
        public string Id { get; set; }
        public ItemDetailViewModel()
        {
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Category
        {
            get => category;
            set => SetProperty(ref category, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }
        public string UserId
        {
            get
            {
                return userId;
            }
            set
            {
                userId = value;
            }
        }


        private static readonly string urlShool = "http://159.223.87.40/school/";
        private static readonly HttpClient client = new HttpClient();
        public async void LoadItemId(string itemId)
        {
            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string> { { "id", itemId } };
                FormUrlEncodedContent form = new FormUrlEncodedContent(dict);
                HttpResponseMessage response = await client.PostAsync(urlShool + $"{ItemId}", form);
                string result = await response.Content.ReadAsStringAsync();
                Dictionary<string, string> jsonUser = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                var item = new Item { Id = jsonUser["id"], Text = jsonUser["name"], Category = jsonUser["level_name"], Description = jsonUser["discription"]  };
                Id = item.Id;
                Text = item.Text;
                Category = item.Category;
                Description = item.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
