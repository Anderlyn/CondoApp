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
    class AddSpaceViewModel : INotifyPropertyChanged
    {
        static HttpClient client = new HttpClient();
        private ICommand addCommand { get; set; }
        public ICommand AddCommand
        {
            get => addCommand;
            set
            {
                addCommand = value;
                OnPropertyChanged();
            }
        }
        private String name { get; set; }
        private String nameError { get; set; }

        private String floor { get; set; }
        private String floorError { get; set; }

        private String imageUrl { get; set; }
        private String imageUrlError { get; set; }

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

        public String Floor
        {
            get => floor;
            set
            {
                floor = value;
                OnPropertyChanged();
            }
        }
        public String FloorError
        {
            get => floorError;
            set
            {
                floorError = value;
                OnPropertyChanged();
            }
        }

        public String ImageUrl
        {
            get => imageUrl;
            set
            {
                imageUrl = value;
                OnPropertyChanged();
            }
        }
        public String ImageUrlError
        {
            get => imageUrlError;
            set
            {
                imageUrlError = value;
                OnPropertyChanged();
            }
        }

        public AddSpaceViewModel()
        {
            AddCommand = new Command(() => {
                NameError = "";
                FloorError = "";
                ImageUrlError = "";
                if (String.IsNullOrEmpty(Name))
                {
                    NameError = "Debes llenar este campo.";
                    return;
                }
                if (String.IsNullOrEmpty(Floor))
                {
                    FloorError = "Debes llenar este campo.";
                    return;
                }
                if (String.IsNullOrEmpty(ImageUrl))
                {
                    ImageUrlError = "Debes llenar este campo.";
                    return;
                }

                ReservationsTypesModel spaceToAdd = new ReservationsTypesModel();
                spaceToAdd.floor = Floor;
                spaceToAdd.imageUrl = ImageUrl;
                spaceToAdd.type = Name;
                CreateType(spaceToAdd);
            });
        }

        public async Task CreateType(ReservationsTypesModel addingSpace)
        {
            var url = API.apiURL + "reservation_types";
            HttpResponseMessage response = await client.PostAsJsonAsync<ReservationsTypesModel>(url, addingSpace);
            try
            {
                response.EnsureSuccessStatusCode();
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
