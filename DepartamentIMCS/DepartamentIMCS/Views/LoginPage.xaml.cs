using DepartamentIMCS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Net;
using System.Net.Http;


using Newtonsoft;
using Newtonsoft.Json;
namespace DepartamentIMCS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public static string login, password;
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }
        void Login_Completed(object sender, EventArgs args)
        {
            login = ((Entry)sender).Text;
        }

        void Password_Completed(object sender, EventArgs args)
        {
            password = ((Entry)sender).Text;
        }
    }
}