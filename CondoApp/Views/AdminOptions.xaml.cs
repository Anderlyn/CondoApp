﻿using CondoApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CondoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminOptions : ContentPage
    {
        public AdminOptions()
        {
            InitializeComponent();
            BindingContext = new AdminOptionsViewModel();
        }
    }
}