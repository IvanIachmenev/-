using DepartamentIMCS.Models;
using DepartamentIMCS.Views;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;

using Xamarin.Forms;
using Newtonsoft;
using Newtonsoft.Json;

namespace DepartamentIMCS.ViewModels
{
    public class PublicationsViewModel : BaseViewModel
    {
        private Publications _selectedItem;

        public ObservableCollection<Publications> Publications { get; }
        public Command LoadTeachersCommand { get; }
        public Command AddTeacherCommand { get; }
        public Command<Publications> ItemTapped { get; }

        public PublicationsViewModel() 
        {
            Title = "Публикации";
            Publications = new ObservableCollection<Publications>();
            LoadTeachersCommand = new Command(async () => await ExecuteLoadTeachersCommand());

            ItemTapped = new Command<Publications>(OnItemSelected);

            AddTeacherCommand = new Command(OnAddTeacher);
        }

        private static readonly string uriPublications = "http://159.223.87.40/all_post";
        private static readonly HttpClient client = new HttpClient();
        async Task ExecuteLoadTeachersCommand()
        {
            IsBusy = true;

            try
            {
                Publications.Clear();
                Dictionary<string, string> dict = new Dictionary<string, string> { };
                FormUrlEncodedContent form = new FormUrlEncodedContent(dict);
                HttpResponseMessage response = await client.PostAsync(uriPublications, form);
                string result = await response.Content.ReadAsStringAsync();
                List<Dictionary<string, string>> jsonPublications = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(result);

                foreach (var publication in jsonPublications)
                {                   
                    Publications.Add(new Publications { id = publication["id"], title = publication["title"], comment = publication["comment"], mediaUrl = publication["media"], user = publication["user"], Category = null });
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

        public Publications SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddTeacher(object obj)
        {

            await Shell.Current.GoToAsync($"{nameof(NewPublicationsPage)}");
        }

        async void OnItemSelected(Publications item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            (Shell.Current as AppShell).CurrentPostIdUser = item.user;
            await Shell.Current.GoToAsync($"{nameof(PublicationsDatailPage)}?{nameof(PublicationsDetailViewModel.ItemId)}={item.id}");
        }

    }
}
