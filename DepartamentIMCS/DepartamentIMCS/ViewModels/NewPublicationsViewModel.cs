using DepartamentIMCS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Net;
using System.Net.Http;

using Newtonsoft;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace DepartamentIMCS.ViewModels
{
    public class NewPublicationsViewModel : BaseViewModel
    {
        private string text;
        private string description;
        private string userId;
        private string mediaUrl;
        public string UserId
        {
            get => userId;
            set => SetProperty(ref userId, value);
        }

        public NewPublicationsViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(text)
                && !String.IsNullOrWhiteSpace(description);
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }
       

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string MediaUrl
        {
            get => mediaUrl;
            set => SetProperty(ref mediaUrl, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private readonly string uriCreatePost = "http://159.223.87.40/create_post";
        private static readonly HttpClient client = new HttpClient();
        private async void OnSave()
        {
            Publications newPublication = new Publications()
            {
                id = Guid.NewGuid().ToString(),
                title = Text,
                comment = Description,
                user = (Shell.Current as AppShell).IdUser,
                mediaUrl = MediaUrl
            };

            string aye = JsonConvert.SerializeObject(newPublication);
            var httpContent = new StringContent(aye, Encoding.UTF8, "application/json");
            HttpResponseMessage response1 = await client.PostAsync(uriCreatePost, httpContent);
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
