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
    public partial class NewTeacherPage : ContentPage
    {
        public Teacher Item { get; set; }
        public NewTeacherPage()
        {
            InitializeComponent();
            BindingContext = new NewTeacherViewModel();
        }
    }
}