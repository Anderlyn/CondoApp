using CondoApp.Models;
using CondoApp.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CondoApp.ViewModels
{
    class OwnedReservationsViewModel : INotifyPropertyChanged
    {
        static HttpClient client = new HttpClient();

        private ObservableCollection<DisplayReservation> reservations { get; set; }

        public ObservableCollection<DisplayReservation> Reservations
        {
            get => reservations;
            set
            {
                reservations = value;
                OnPropertyChanged();
            }
        }

        private String displayName { get; set; }
        public String DisplayName
        {
            get => displayName;
            set
            {
                displayName = value;
            }
        }

        public OwnedReservationsViewModel()
        {
            DisplayName = UserDetails.name + " " + UserDetails.last_name;
            GetAppointments();
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

        public async Task DeleteAppointment(string id)
        {
            IsFetching = true;
            IsLoaded = false;
            var url = API.apiURL + "reservation/delete";
            IDJson deleteId = new IDJson();
            deleteId.id = id;
            HttpResponseMessage response = await client.PostAsJsonAsync(url, deleteId);
            try
            {
                response.EnsureSuccessStatusCode();
                GetAppointments();
            }
            catch (HttpRequestException error)
            {
                IsFetching = false;
                IsLoaded = true;

            }
        }

        public async Task GetAppointments()
        {

            var url = API.apiURL + "reservation/userwise";
            ApartmentJson searchApartment = new ApartmentJson();
            searchApartment.apartment = UserDetails.apartment;
            HttpResponseMessage response = await client.PostAsJsonAsync(url, searchApartment);
            try
            {
                DisplayReservation[] fetchedReservations = await response.Content.ReadAsAsync<DisplayReservation[]>();
                response.EnsureSuccessStatusCode();
                ObservableCollection<DisplayReservation> userReservations = new ObservableCollection<DisplayReservation>();

                foreach (DisplayReservation appointment in fetchedReservations)
                {
                    appointment.formattedDate = new DateTime(appointment.year, appointment.month, appointment.day, appointment.hour, 0, 0).ToString();
                    appointment.deleteCommand = new Command(() =>
                    {
                        DeleteAppointment(appointment.id);
                    });
                    userReservations.Add(appointment);
                }

                Reservations = userReservations;
                IsFetching = false;
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
