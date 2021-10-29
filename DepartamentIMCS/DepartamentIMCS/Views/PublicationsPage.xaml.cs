using DepartamentIMCS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DepartamentIMCS.Views
{
    public partial class PublicationsPage : ContentPage
    {
        PublicationsViewModel _viewModel;
        public PublicationsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new PublicationsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }   
    }
}