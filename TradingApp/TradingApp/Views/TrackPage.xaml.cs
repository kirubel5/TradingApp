using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TradingApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrackPage : ContentPage
    {
        private readonly TrackViewModel trackViewModel;

        public TrackPage()
        {
            InitializeComponent();

            trackViewModel = new TrackViewModel();
            BindingContext = trackViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await trackViewModel.Load();
        }
    }
}