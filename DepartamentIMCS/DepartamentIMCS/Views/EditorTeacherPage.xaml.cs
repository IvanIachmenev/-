using DepartamentIMCS.Models;
using DepartamentIMCS.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DepartamentIMCS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditorTeacherPage : ContentPage
    {
        public Publications Publications { get; set; }
        public EditorTeacherPage()
        {
            InitializeComponent();
            BindingContext = new EditorTeacherViewModel();
        }
    }
}