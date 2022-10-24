using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingApp.Services;
using TradingApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TradingApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TradePage : ContentPage
    {
        private readonly TradeViewModel tradeViewModel;

        public TradePage()
        {
            InitializeComponent();
            tradeViewModel = new TradeViewModel();
            BindingContext = tradeViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await tradeViewModel.OnAppearingDisplay();
        }
    }
}