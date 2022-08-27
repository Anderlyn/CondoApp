using CondoApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CondoApp.Utils
{
    public class DisplayReservation 
    {
        public int year { get; set; }
        public int day { get; set; }
        public int month { get; set; }
        public int hour { get; set; }
        public String type { get; set; }
        public String apartment { get; set; }
        public String formattedDate { get; set; }
        public String id { get; set; }
        public ICommand deleteCommand { get; set; }
    }
}
