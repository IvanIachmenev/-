using DepartamentIMCS.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;


namespace DepartamentIMCS.Views
{
    public partial class TeacherDatailPage : ContentPage
    {
        public TeacherDatailPage()
        {
            InitializeComponent();
            BindingContext = new TeacherDetailViewModel();
            Routing.RegisterRoute(nameof(EditorTeacherPage), typeof(EditorTeacherPage));
            ToolbarItems.Remove(Edite);
            
            /*if((Shell.Current as AppShell).Category == null)
            {
                if((Shell.Current as AppShell).UserName != FIO.Text)
                {
                    ToolbarItems.Remove(Edite);
                }
            }*/
            
        }
    }
}