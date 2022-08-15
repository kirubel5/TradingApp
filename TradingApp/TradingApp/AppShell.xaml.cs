using System;
using System.Collections.Generic;
using TradingApp.ViewModels;
using TradingApp.Views;
using Xamarin.Forms;

namespace TradingApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(AddNewTrackPage), typeof(AddNewTrackPage));
            Routing.RegisterRoute(nameof(AddNewTradePage), typeof(AddNewTradePage));
        }

    }
}
