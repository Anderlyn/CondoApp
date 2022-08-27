using CondoApp.Models;
using CondoApp.Utils;
using CondoApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CondoApp.ViewModels
{
    class HomeViewModel : INotifyPropertyChanged
    {
        private String displayName { get; set; }
        public String DisplayName { 
            get => displayName; 
            set {
                displayName = value;
            } 
        }

        private Boolean isAdmin { get; set; }
        public Boolean IsAdmin
        {
            get => isAdmin;
            set
            {
                isAdmin = value;
                isAdmin = value;
            }
        }

        public ICommand exitCommand { get; set; }
        public ICommand reservationsCommand { get; set; }

        public ICommand adminCommand { get; set; }

        public ICommand informationCommand { get; set; }

        public HomeViewModel()
        {
            DisplayName = UserDetails.name + " " + UserDetails.last_name;
            IsAdmin = UserDetails.role.Equals("admin");
            exitCommand = new Command(() => {
                GoToLogin();
            });
            reservationsCommand = new Command(() => {
                GoToReservations();
            });
            adminCommand = new Command(() =>
            {
                GoToAdmin();
            });
            informationCommand = new Command(() =>
            {
                GoToInformation();
            });
        }


        public async void GoToAdmin()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Views.AdminOptions());

        }

        public async void GoToInformation()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Views.Information());

        }

        public async void GoToReservations()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Reservations());

        }
        public async void GoToLogin()
        {
            UserDetails.uid = null;
            UserDetails.email = null;
            UserDetails.apartment = null;
            UserDetails.name = null;
            UserDetails.role = null;
            UserDetails.last_name = null;
            await Application.Current.MainPage.Navigation.PushAsync(new CondoLogin());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
