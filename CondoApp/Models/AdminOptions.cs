using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CondoApp.Models
{
    class AdminOptions
    {
        public String option { get; set; }
        public ICommand openCommand { get; set; }
    }
}
