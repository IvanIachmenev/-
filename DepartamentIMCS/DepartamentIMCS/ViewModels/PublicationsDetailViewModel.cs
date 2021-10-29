using DepartamentIMCS.Views;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;

using Xamarin.Forms;
using Newtonsoft;
using Newtonsoft.Json;
using DepartamentIMCS.Models;
using System.Collections.Generic;

namespace DepartamentIMCS.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class PublicationsDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string text;
        private string description;
        private string category;
        private string imgUri;
        public string Id { get; set; }

        public PublicationsDetailViewModel()
        {
            DeleteCommand = new Command(OnDelete);
            EditeCommand = new Command(OnEditeItem);
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
        public string ImgUri
        {
            get => imgUri;
            set => SetProperty(ref imgUri, value);
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


        public Command DeleteCommand { get; }
        public readonly string uriDeletePost = "http://159.223.87.40/remove_post/";
        public async void OnDelete()
        {
            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string> { { "id", itemId } };
                FormUrlEncodedContent form = new FormUrlEncodedContent(dict);
                HttpResponseMessage response = await client.PostAsync(uriDeletePost + $"{ItemId}", form);
            }
            catch
            {
                Debug.WriteLine("Failed to Delete Publication");
            }

            await Shell.Current.GoToAsync("..");
        }

        public Command EditeCommand { get; }
        private async void OnEditeItem(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(EditorTeacherPage)}?{nameof(EditorTeacherViewModel.ItemId)}={ItemId}");
        }

        private static readonly string uriPosts = "http://159.223.87.40/post/";
        private static readonly HttpClient client = new HttpClient();
        public async void LoadItemId(string itemId)
        {
            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string> { { "id", itemId } };
                FormUrlEncodedContent form = new FormUrlEncodedContent(dict);
                HttpResponseMessage response = await client.PostAsync(uriPosts + $"{ItemId}", form);
                string result = await response.Content.ReadAsStringAsync();
                Dictionary<string, string> jsonUser = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                
                var publication = new Publications { id = jsonUser["id"], title = jsonUser["title"], comment = jsonUser["comment"], mediaUrl = jsonUser["media_url"] };
                Id = publication.id;
                Text = publication.title;
                Description = publication.comment;
                imgUri = publication.mediaUrl;
         
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
