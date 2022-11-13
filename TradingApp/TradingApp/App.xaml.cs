using System;
using System.IO;
using TradingApp.Services;
using TradingApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TradingApp
{
    public partial class App : Application
    {
        static TradingDatabase database;

        // Create the database connection as a singleton.
        public static TradingDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new TradingDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TradeData.db3"));
                }
                return database;
            }
        }

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Njk2NzMyQDMyMzAyZTMyMmUzMEQxN0EybTc4VTJxSEt0Ylo3M1hjODZEVE1pZU5yWkY5RjhRY3B4TWFVZEU9");
           
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected async override void OnStart()
        {
            base.OnStart();
            await Shell.Current.GoToAsync("//ArbitrageTrackPage");
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
