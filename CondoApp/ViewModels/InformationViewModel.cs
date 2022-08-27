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
    class InformationViewModel : INotifyPropertyChanged
    {
        static HttpClient client = new HttpClient();

        private ICommand updateCommand { get; set; }
        public ICommand UpdateCommand
        {
            get => updateCommand;
            set
            {
                updateCommand = value;
                OnPropertyChanged();
            }
        }

        private String name { get; set; }
        private String nameError { get; set; }

        private String lastName { get; set; }
        private String lastNameError { get; set; }

        private String apartment { get; set; }

        private String email { get; set; }

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

        public String Email
        {
            get => email;
            set
            {
                email = value;
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

        public InformationViewModel()
        {
            Apartment = UserDetails.apartment;
            Email = UserDetails.email;
            LastName = UserDetails.last_name;
            Name = UserDetails.name;
            UpdateCommand = new Command(() => {
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
                UserDetailsModel newData = new UserDetailsModel();
                newData.name = Name;
                newData.last_name = LastName;
                newData.uid = UserDetails.uid;
                UpdateData(newData);
            });
        }

        public async Task UpdateData(UserDetailsModel updateData) {
            var url = API.apiURL + "user";
            HttpResponseMessage response = await client.PutAsJsonAsync<UserDetailsModel>(url, updateData);
            try
            {
                response.EnsureSuccessStatusCode();
                UserDetails.name = Name;
                UserDetails.last_name = LastName;
                await Application.Current.MainPage.Navigation.PushAsync(new Home());
            }
            catch (Exception error)
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
