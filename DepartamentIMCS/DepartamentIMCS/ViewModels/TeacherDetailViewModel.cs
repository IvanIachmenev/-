using DepartamentIMCS.Views;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

using Xamarin.Forms;
using Newtonsoft;
using Newtonsoft.Json;
using DepartamentIMCS.Models;

namespace DepartamentIMCS.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class TeacherDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string text;
        private string description;
        private string category;
        private string imgUri;
        public string Id { get; set; }

        public TeacherDetailViewModel()
        {
        }
        public string ImgUri
        {
            get => imgUri;
            set => SetProperty(ref imgUri, value);
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



        private static readonly string urlShool = "http://159.223.87.40/user/";
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
                var teacher = new Teacher { Id = jsonUser["id"], Text = jsonUser["username"], Category = jsonUser["level_name"], Description = jsonUser["bio"], ImgUri= jsonUser["avatar"] };
                Id = teacher.Id;
                Text = teacher.Text;
                Description = teacher.Description;
                ImgUri = teacher.ImgUri;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
