using DepartamentIMCS.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft;
using Newtonsoft.Json;

namespace DepartamentIMCS.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        Entry loginEntry, passwordEntry;
        public LoginViewModel()
        {
            StackLayout stackLayout = new StackLayout();

            
            loginEntry = new Entry { Placeholder = "Login" };

            passwordEntry = new Entry
            {
                Placeholder = "Password",
                IsPassword = true
            };
            

            LoginCommand = new Command(OnLoginClicked);
        }
        
        private static readonly HttpClient client = new HttpClient();
        private async void OnLoginClicked(object obj)
        {
            User user = new User { username = LoginPage.login, password = LoginPage.password };
            string aye = JsonConvert.SerializeObject(user);
            var httpContent = new StringContent(aye, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync((Shell.Current as AppShell).uriUser, httpContent);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string result = await response.Content.ReadAsStringAsync();
                Dictionary<string, string> jsonUser = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                (Shell.Current as AppShell).IdUser = jsonUser["id"];
                if (Convert.ToBoolean(jsonUser["isAdmin"]))
                {
                    (Shell.Current as AppShell).Admin = true;
                }
                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}?{nameof(AboutViewModel)}");
            }
            else
            {
                await (Shell.Current as AppShell).DisplayAlert("Ошибка", "Наверный логин или пароль", "Ок");
            }


            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
        }
    }

    public class User
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
