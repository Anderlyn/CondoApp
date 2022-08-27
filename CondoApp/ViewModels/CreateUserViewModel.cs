using CondoApp.Models;
using CondoApp.Utils;
using CondoApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CondoApp.ViewModels
{
    class CreateUserViewModel : INotifyPropertyChanged
    {
        static HttpClient client = new HttpClient();

        private ICommand addCommand { get; set; }
        private String email { get; set; }
        private String emailError { get; set; }

        private String name { get; set; }
        private String nameError { get; set; }

        private String lastName { get; set; }
        private String lastNameError { get; set; }

        private String apartment { get; set; }
        private String apartmentError { get; set; }


        public ICommand AddCommand
        {
            get => addCommand;
            set
            {
                addCommand = value;
                OnPropertyChanged();
            }
        }

        public String Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        public String EmailError
        {
            get => emailError;
            set
            {
                emailError = value;
                OnPropertyChanged();
            }
        }
        public String Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        public String NameError
        {
            get => nameError;
            set
            {
                nameError = value;
                OnPropertyChanged();
            }
        }


        public String LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged();
            }
        }

        public String LastNameError
        {
            get => lastNameError;
            set
            {
                lastNameError = value;
                OnPropertyChanged();
            }
        }
        public String Apartment
        {
            get => apartment;
            set
            {
                apartment = value;
                OnPropertyChanged();
            }
        }
        public String ApartmentError
        {
            get => apartmentError;
            set
            {
                apartmentError = value;
                OnPropertyChanged();
            }
        }

        public CreateUserViewModel()
        {
            AddCommand = new Command(() => {
                NameError = "";
                LastNameError = "";
                EmailError = "";
                ApartmentError = "";
                if (String.IsNullOrEmpty(Name))
                {
                    NameError = "Debes llenar este campo.";
                    return;
                }
                if (String.IsNullOrEmpty(LastName))
                {
                    LastNameError = "Debes llenar este campo.";
                    return;
                }
                if (String.IsNullOrEmpty(Email))
                {
                    EmailError = "Debes llenar este campo.";
                    return;
                }
                if (String.IsNullOrEmpty(Apartment))
                {
                    ApartmentError = "Debes llenar este campo.";
                    return;
                }

                UserDetailsModel userToAdd = new UserDetailsModel();
                userToAdd.email = Email;
                userToAdd.apartment = Apartment;
                userToAdd.name = Name;
                userToAdd.last_name = LastName;
                CreateUser(userToAdd);
            });
        }

        public async Task CreateUser(UserDetailsModel newUser)
        {
            var url = API.apiURL + "user/create";
            HttpResponseMessage response = await client.PostAsJsonAsync<UserDetailsModel>(url, newUser);
            try
            {
                response.EnsureSuccessStatusCode();
                await Application.Current.MainPage.Navigation.PushAsync(new Home());
            }catch(Exception error)
            {

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
