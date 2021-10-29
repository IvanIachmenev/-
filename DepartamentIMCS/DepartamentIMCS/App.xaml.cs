using DepartamentIMCS.Services;
using DepartamentIMCS.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DepartamentIMCS
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<MockTeachers>();
            DependencyService.Register<DataPublications>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
