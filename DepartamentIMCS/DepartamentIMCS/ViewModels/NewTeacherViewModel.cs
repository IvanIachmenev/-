using DepartamentIMCS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DepartamentIMCS.ViewModels
{
    public class NewTeacherViewModel : BaseViewModel
    {
        private string text;
        private string category;
        private string description;

        public NewTeacherViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(text)
                && !String.IsNullOrWhiteSpace(description) && !String.IsNullOrWhiteSpace(category);
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }
        public string Category
        {
            get => category;
            set => SetProperty(ref category, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Teacher newItem = new Teacher()
            {
                Id = Guid.NewGuid().ToString(),
                Text = Text,
                Category = Category,
                Description = Description
            };

            await DataTeachers.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
