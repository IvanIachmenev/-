using DepartamentIMCS.ViewModels;
using DepartamentIMCS.Views;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

using Xamarin.Forms;
using Newtonsoft;
using Newtonsoft.Json;

namespace DepartamentIMCS
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        
        public string uriUser = "http://159.223.87.40/login";
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(TeacherDatailPage), typeof(TeacherDatailPage));
            Routing.RegisterRoute(nameof(NewTeacherPage), typeof(NewTeacherPage));
            Routing.RegisterRoute(nameof(PublicationsDatailPage), typeof(PublicationsDatailPage));
            Routing.RegisterRoute(nameof(NewPublicationsPage), typeof(NewPublicationsPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }

        public string IdUser = "";
        public bool IsLogged = false;
        public bool Admin = false;
        public string Category;
        public string UserName;
        public string CurrentPostIdUser;

    }
}
