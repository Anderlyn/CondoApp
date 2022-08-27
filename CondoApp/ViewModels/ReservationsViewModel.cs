using CondoApp.Models;
using CondoApp.Utils;
using CondoApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CondoApp.ViewModels
{
    class ReservationsViewModel : INotifyPropertyChanged
    {
        static HttpClient client = new HttpClient();
        private ObservableCollection<ReservationsTypesModel> reservationTypes { get; set; }

        private ICommand ownedCommand { get; set; }

        public ICommand OwnedCommand
        {
            get => ownedCommand;
            set
            {
                ownedCommand = value;
                OnPropertyChanged();
            }
        }
        private Boolean isFetching { get; set; } 
        private Boolean isLoaded { get; set; }

        public Boolean IsFetching
        {
            get => isFetching;
            set
            {
                isFetching = value;
                OnPropertyChanged();
            }
        }

        public Boolean IsLoaded
        {
            get => isLoaded;
            set
            {
                isLoaded = value;
                OnPropertyChanged();
            }

        }

        public ObservableCollection<ReservationsTypesModel> ReservationTypes
        {
            get { return reservationTypes; }
            set { reservationTypes = value; OnPropertyChanged(); }
        }

        public ReservationsViewModel()
        {
            IsFetching = true;
            IsLoaded = false;
            OwnedCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PushAsync(new OwnedReservations());

            });
            GetTypes();
        }


        public async Task GetTypes()
        {
            var url = API.apiURL + "reservation_types";
            HttpResponseMessage response = await client.GetAsync(url);
            try
            {
                response.EnsureSuccessStatusCode();
                ReservationsTypesModel[] types = await response.Content.ReadAsAsync<ReservationsTypesModel[]>();
                IsFetching = false;
                ObservableCollection<ReservationsTypesModel> defaultReservationTypes = new ObservableCollection<ReservationsTypesModel>();

                foreach(ReservationsTypesModel type in types)
                {
                    type.reserveCommand = new Command(async () => {
                        await Application.Current.MainPage.Navigation.PushAsync(new ReservationCalendar(type.type));
                    });
                    defaultReservationTypes.Add(type);
                }

                ReservationTypes = defaultReservationTypes;
                IsLoaded = true;
            }
            catch (HttpRequestException error)
            {
                IsFetching = false;
                IsLoaded = true;

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
