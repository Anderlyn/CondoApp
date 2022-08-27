using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CondoApp.Models
{
    class ReservationsTypesModel
    {
        public String type { get; set; }
        public String floor { get; set; }

        public String imageUrl { get; set; }
        public ICommand reserveCommand { get; set; }
    }
}
