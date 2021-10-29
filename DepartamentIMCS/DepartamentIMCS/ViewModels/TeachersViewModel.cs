using DepartamentIMCS.Models;
using DepartamentIMCS.Views;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;

using Newtonsoft;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace DepartamentIMCS.ViewModels
{
    public class TeachersViewModel : BaseViewModel
    {
        private Teacher _selectedItem;

        public ObservableCollection<Teacher> Teachers { get; }
        public Command LoadTeachersCommand { get; }
        public Command AddTeacherCommand { get; }
        public Command<Teacher> ItemTapped { get; }

        public TeachersViewModel()
        {
            Title = "Преподователи";
            Teachers = new ObservableCollection<Teacher>();
            LoadTeachersCommand = new Command(async () => await ExecuteLoadTeachersCommand());

            ItemTapped = new Command<Teacher>(OnItemSelected);

            AddTeacherCommand = new Command(OnAddTeacher);

        }

        private static readonly string uriTeachers = "http://159.223.87.40/admins";
        private static readonly HttpClient client = new HttpClient();
        async Task ExecuteLoadTeachersCommand()
        {
            IsBusy = true;

            try
            {
                Teachers.Clear();
                Dictionary<string, string> dict = new Dictionary<string, string> { };
                FormUrlEncodedContent form = new FormUrlEncodedContent(dict);
                HttpResponseMessage response = await client.PostAsync(uriTeachers, form);
                string result = await response.Content.ReadAsStringAsync();
                List<Dictionary<string, string>> jsonTeahcers = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(result);

                foreach (var teacher in jsonTeahcers)
                {
                    Teachers.Add(new Teacher { Id = teacher["id"], Text = teacher["username"], Category = teacher["level_name"], Description = teacher["bio"], ImgUri= teacher["avatar"] });    
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

        public Teacher SelectedItem
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

            await Shell.Current.GoToAsync($"{nameof(NewTeacherPage)}");
        }

        async void OnItemSelected(Teacher item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(TeacherDatailPage)}?{nameof(TeacherDetailViewModel.ItemId)}={item.Id}");
        }
    }
}
