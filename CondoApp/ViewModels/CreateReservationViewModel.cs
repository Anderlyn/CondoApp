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
    class CreateReservationViewModel : INotifyPropertyChanged
    {

        static HttpClient client = new HttpClient();

        private String reservationType { get; set; }
        private DateTime selectedDate { get; set; }
        private TimeSpan selectedHour { get; set; }

        private Boolean isIncorrect { get; set; }

        public Boolean IsIncorrect
        {
            get => isIncorrect;
            set
            {
                isIncorrect = value;
                OnPropertyChanged();
            }
        }

        private Boolean isLoading { get; set; }

        public Boolean IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;
                OnPropertyChanged();
            }
        }
        public DateTime SelectedDate
        {
            get => selectedDate;
            set
            {
                selectedDate = value;
                OnPropertyChanged();
            }
        }

        private ICommand saveCommand { get; set; }

        public ICommand SaveCommand
        {
            get => saveCommand;
            set
            {
                saveCommand = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan SelectedHour
        {
            get => selectedHour;
            set
            {
                selectedHour = value;
                OnPropertyChanged();
            }
        }

        public String ReservationType
        {
            get => reservationType;
            set
            {
                reservationType = value;
                OnPropertyChanged();
            }
        }

        public CreateReservationViewModel(Reservation[] currentReservations, String type)
        {
            ReservationType = type + " >";
            IsIncorrect = false;
            SelectedDate = DateTime.Now;
            SelectedHour = new TimeSpan(12, 0, 0);
            SaveCommand = new Command(() => {

                foreach(Reservation reservation in currentReservations)
                {
                    if((selectedDate.Day == reservation.day) && (selectedDate.Month == reservation.month) && (selectedDate.Year == reservation.year) && (selectedHour.Hours == reservation.hour))
                    {
                        IsIncorrect = true;
                        return;
                    }
                }

                Reservation reservationToSave = new Reservation();
                reservationToSave.day = selectedDate.Day;
                reservationToSave.apartment = UserDetails.apartment;
                reservationToSave.type = type;
                reservationToSave.hour = selectedHour.Hours;
                reservationToSave.month = selectedDate.Month;
                reservationToSave.year = selectedDate.Year;

                SaveReservation(reservationToSave);
            });
        }

        public async Task SaveReservation(Reservation newReservation)
        {
            IsLoading = true;
            var url = API.apiURL + "reservation";
            HttpResponseMessage response = await client.PostAsJsonAsync<Reservation>(url, newReservation);
            try
            {
                response.EnsureSuccessStatusCode();
                await Application.Current.MainPage.Navigation.PushAsync(new Home());

            }
            catch (HttpRequestException error)
            {
                IsLoading = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public CreateReservationViewModel() { }
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
