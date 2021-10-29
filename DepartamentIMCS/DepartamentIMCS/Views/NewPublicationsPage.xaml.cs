using DepartamentIMCS.Models;
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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewPublicationsPage : ContentPage
    {
        public Publications Item { get; set; }
        public NewPublicationsPage()
        {
            InitializeComponent();
            BindingContext = new NewPublicationsViewModel();
            
        }

    }
}