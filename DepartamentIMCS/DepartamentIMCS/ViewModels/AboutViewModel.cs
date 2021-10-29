using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using DepartamentIMCS.Views;

namespace DepartamentIMCS.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        public AboutViewModel()
        {
            Title = "Дерпатамент МКН";
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
        }
    }
}