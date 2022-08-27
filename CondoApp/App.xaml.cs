using CondoApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syncfusion;

namespace CondoApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NzAxMTA3QDMyMzAyZTMyMmUzMEM2SjlPc2dsNFFuSGxSSHcxZm9ybHVHN0o5VHc1OVp1dFFiaDV1ZHlNeW89");

            MainPage = new NavigationPage(new CondoLogin());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
