using DepartamentIMCS.Models;
using DepartamentIMCS.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using System.Net;
using System.Net.Http;

using Newtonsoft;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace DepartamentIMCS.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    class EditorTeacherViewModel : BaseViewModel
    {
        private string itemId;
        private string text;
        private string description;
        private string imgUrl;
        private string user;

        public string Id { get; set; }
        public EditorTeacherViewModel()
        {

            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }
        public string User
        {
            get => user;
            set => SetProperty(ref user, value);
        }
        public string ImgUrl
        {
            get => imgUrl;
            set => SetProperty(ref imgUrl, value);
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

        private static readonly string uriPosts = "http://159.223.87.40/post/";
        private static readonly HttpClient client = new HttpClient();
        public async void LoadItemId(string itemId)
        {
            try
            {
               /* Dictionary<string, string> dict = new Dictionary<string, string> { { "id", itemId } };
                FormUrlEncodedContent form = new FormUrlEncodedContent(dict);
                HttpResponseMessage response = await client.PostAsync(uriPosts + $"{ItemId}", form);
                string result = await response.Content.ReadAsStringAsync();
                Dictionary<string, string> jsonUser = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);

                var publication = new Publications { Id = jsonUser["id"], Text = jsonUser["title"], Description = jsonUser["comment"], ImgUri = jsonUser["media_url"] };
                Id = publication.Id;
                Text = publication.Text;
                Description = publication.Description;*/
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(text)
                && !String.IsNullOrWhiteSpace(description);
        }


        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            Debug.WriteLine(Id);
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private readonly string uriEditPost = "http://159.223.87.40/edit_post/";
        private async void OnSave()
        {
            Dictionary<string, string> dict = new Dictionary<string, string> { { "id", itemId } };
            FormUrlEncodedContent form = new FormUrlEncodedContent(dict);
            HttpResponseMessage response = await client.PostAsync(uriPosts + $"{ItemId}", form);
            string result = await response.Content.ReadAsStringAsync();

            Dictionary<string, string> jsonUser = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);

            var publication = new Publications { id = jsonUser["id"], title = jsonUser["title"], comment = jsonUser["comment"], mediaUrl = jsonUser["media_url"], user=jsonUser["user"] };
            Id = publication.id;

            Publications newPublication = new Publications()
            {
                id = Id,
                title = Text,
                comment = Description,
                mediaUrl = jsonUser["media_url"],
                user = jsonUser["user"]
            };
            string aye = JsonConvert.SerializeObject(newPublication);
            var httpContent = new StringContent(aye, Encoding.UTF8, "application/json");
            HttpResponseMessage response1 = await client.PostAsync(uriEditPost + $"{ItemId}", httpContent);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync($"//{nameof(PublicationsPage)}");
        }
    }
}
