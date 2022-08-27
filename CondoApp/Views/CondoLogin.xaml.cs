using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.SfCalendar;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CondoApp.ViewModels;

namespace CondoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CondoLogin : ContentPage
    {
        public void Add() { }
        public CondoLogin()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            BindingContext = new LoginViewModel(Navigation);
        }
    }
}