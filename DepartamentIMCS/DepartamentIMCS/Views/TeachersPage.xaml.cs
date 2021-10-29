using DepartamentIMCS.Models;
using DepartamentIMCS.ViewModels;
using DepartamentIMCS.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DepartamentIMCS.Views
{
    public partial class TeachersPage : ContentPage
    {
        TeachersViewModel _viewModel;
        public TeachersPage()
        {
            InitializeComponent();
            //ToolbarItems.Remove(Add);

            BindingContext = _viewModel = new TeachersViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}