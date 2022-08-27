using CondoApp.Models;
using CondoApp.Utils;
using CondoApp.Views;
using Syncfusion.SfCalendar.XForms;
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
    class ReservationCalendarViewModel : INotifyPropertyChanged
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


        private String reservationType { get; set; }

        private DateTime minDate { get; set; }

        private Boolean isFetching { get; set; }
        private Boolean isLoaded { get; set; }
        
        private Reservation[] currentReservations { get; set; }

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

        private CalendarEventCollection appointments { get; set; }

        public DateTime MinDate
        {
            get => minDate;
            set
            {
                minDate = value;
                OnPropertyChanged();
            }
        }
        public CalendarEventCollection Appointments
        {
            get => appointments;
            set
            {
                appointments = value;
                OnPropertyChanged();
            }
        }
        public String ReservationType { 
            get => reservationType; 
            set
            {
                reservationType = value;

            }
        }

        public async Task GetTypes(TypeJson type)
        {
            var url = API.apiURL + "reservation/typewise";
            HttpResponseMessage response = await client.PostAsJsonAsync(url, type);
            try
            {
                Reservation[] types = await response.Content.ReadAsAsync<Reservation[]>();
                response.EnsureSuccessStatusCode();
                IsFetching = false;
                currentReservations = types;
                CalendarEventCollection newAppointments = new CalendarEventCollection();
                foreach (Reservation reservation in types)
                {
                    CalendarInlineEvent currentLoopEvent = new CalendarInlineEvent();
                    currentLoopEvent.StartTime = new DateTime(2022, reservation.month, reservation.day, reservation.hour, 0, 0);
                    currentLoopEvent.EndTime = new DateTime(2022, reservation.month, reservation.day, reservation.hour+1, 0, 0);
                    currentLoopEvent.Color = Color.Purple;
                    currentLoopEvent.Subject = reservation.apartment;
                    newAppointments.Add(currentLoopEvent);
                }

                Appointments = newAppointments;
                IsLoaded = true;
                IsFetching = false;

                AddCommand = new Command(async () =>
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new CreateReservation(currentReservations, reservationType));
                });
            }
            catch (HttpRequestException error)
            {
                IsFetching = false;
                IsLoaded = true;

            }
        }

        public ReservationCalendarViewModel(String type)
        {
            ReservationType = type;
            MinDate = DateTime.Now;
            IsFetching = true;
            IsLoaded = false;
            TypeJson fetchType = new TypeJson();
            fetchType.type = type;
            GetTypes(fetchType);

        }
        public ReservationCalendarViewModel()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
