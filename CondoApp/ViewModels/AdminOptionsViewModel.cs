using CondoApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace CondoApp.ViewModels
{
    class AdminOptionsViewModel : INotifyPropertyChanged
    {

        

        private ObservableCollection<AdminOptions> menuOptions { get; set; }

        public ObservableCollection<AdminOptions> MenuOptions {
            get => menuOptions;
            set
            {
                menuOptions = value;
                OnPropertyChanged();
            }
        }


        public AdminOptionsViewModel()
        {
            ObservableCollection<AdminOptions> defaultOptions = new ObservableCollection<AdminOptions>();
            defaultOptions.Add(new AdminOptions() { option = "Crear Usuario", openCommand = new Command(async () => { await Application.Current.MainPage.Navigation.PushAsync(new Views.CreateUser());  }) });
            defaultOptions.Add(new AdminOptions() { option = "Crear Espacio Publico", openCommand = new Command(async () => { await Application.Current.MainPage.Navigation.PushAsync(new Views.AddSpace()); }) });
            MenuOptions = defaultOptions;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
