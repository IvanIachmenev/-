using DepartamentIMCS.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace DepartamentIMCS.Views
{
    public partial class PublicationsDatailPage : ContentPage
    {
        public PublicationsDatailPage()
        {
            InitializeComponent();
            BindingContext = new PublicationsDetailViewModel();
            if((Shell.Current as AppShell).CurrentPostIdUser != (Shell.Current as AppShell).IdUser)
            {
                ToolbarItems.Remove(Edite);
                ToolbarItems.Remove(Delete);
            }
            else
            {
                Routing.RegisterRoute(nameof(EditorTeacherPage), typeof(EditorTeacherPage));
            }
        }
    }
}