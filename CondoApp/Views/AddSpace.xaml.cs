using CondoApp.ViewModels;
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
    public partial class AddSpace : ContentPage
    {
        public AddSpace()
        {
            InitializeComponent();
            BindingContext = new AddSpaceViewModel();
        }
    }
}