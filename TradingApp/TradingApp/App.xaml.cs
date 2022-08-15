using System;
using TradingApp.Services;
using TradingApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TradingApp
{
    public partial class App : Application
    {

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Njk2NzMyQDMyMzAyZTMyMmUzMEQxN0EybTc4VTJxSEt0Ylo3M1hjODZEVE1pZU5yWkY5RjhRY3B4TWFVZEU9");
           
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected async override void OnStart()
        {
            base.OnStart();
            await Shell.Current.GoToAsync("//TrackPage");
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
