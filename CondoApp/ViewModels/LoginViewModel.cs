using CondoApp.Models;
using CondoApp.Utils;
using CondoApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CondoApp.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {
        static HttpClient client = new HttpClient();
        private Login currentLoginData { get; set; }

        private String emailError { get; set; }

        private String passwordError { get; set; }
        public ICommand loginCommand { get; set; }
        public INavigation Navigation { get; set; }

        public Boolean isChecking { get; set; }

        private Boolean isIncorrect { get; set; }


        public Boolean IsIncorrect
        {
            get => isIncorrect;
            set { isIncorrect = value; OnPropertyChanged(); }
        }
        public Boolean IsChecking {
            get => isChecking;
            set { isChecking = value; OnPropertyChanged(); } 
        }

        public String Email
        {
            get => currentLoginData.email;
            set { currentLoginData.email = value; }
        }

        public String Password
        {
            get => currentLoginData.password;
            set { currentLoginData.password = value;  }
        }

        public String PasswordError
        {
            get => passwordError;
            set { passwordError = value; OnPropertyChanged(); }
        }

        public String EmailError
        {
            get => emailError;
            set { emailError = value; OnPropertyChanged(); }
        }
        public LoginViewModel(INavigation navigation)
        {
            Navigation = navigation;
            IsChecking = true;
            IsIncorrect = false;
            currentLoginData = new Login();
            loginCommand = new Command(() => {
                IsChecking = false;
                EmailError = "";
                PasswordError = "";
                OnPropertyChanged();
                if (String.IsNullOrEmpty(Email))
                {
                    EmailError = "Debes llenar este campo.";
                    IsChecking = true;
                    return;
                }
                if (String.IsNullOrEmpty(Password))
                {
                    PasswordError = "Debes llenar este campo.";
                    IsChecking = true;
                    return;
                }
                LoginUser(currentLoginData);
            });
        }

        public LoginViewModel() { }


        public async Task LoginUser(Login loginAttempt)
        {
            var url = API.apiURL + "login";
            HttpResponseMessage response = await client.PostAsJsonAsync(url, loginAttempt);
            try
            {
                response.EnsureSuccessStatusCode();
                var loginDetails = await response.Content.ReadAsAsync<UserDetailsModel>();
                GetUserData(new UIDJson(loginDetails.uid));
            }
            catch(HttpRequestException error)
            {
                IsChecking = true;
                IsIncorrect = true;
                
            }
        }

        public async Task GetUserData(UIDJson uid)
        {
            var url = API.apiURL + "user";
            HttpResponseMessage response = await client.PostAsJsonAsync(url, uid);
            try {
                response.EnsureSuccessStatusCode();
            }
            catch(HttpRequestException error)
            {
                IsChecking = true;
                IsIncorrect = true;
            }
            UserDetailsModel userDetails = await response.Content.ReadAsAsync<UserDetailsModel>();
            UserDetails.uid = userDetails.uid;
            UserDetails.email = userDetails.email;
            UserDetails.apartment = userDetails.apartment;
            UserDetails.name = userDetails.name;
            UserDetails.role = userDetails.role;
            UserDetails.last_name = userDetails.last_name;
            IsChecking = false;
            await Application.Current.MainPage.Navigation.PushAsync(new Home());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
