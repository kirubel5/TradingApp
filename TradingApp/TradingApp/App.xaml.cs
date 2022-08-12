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
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjE2MjM3QDMyMzAyZTMxMmUzMFZWVEI2TFhmemxZSWVJVVphTExLQ1B5a1VHdngvY0ljcmtaMmZxa2ZPQjQ9");

            InitializeComponent();

            MainPage = new AppShell();
        }

        protected async override void OnStart()
        {
            base.OnStart();
            await Shell.Current.GoToAsync("//TradePage");
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
