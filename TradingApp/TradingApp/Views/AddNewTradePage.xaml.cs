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
    public partial class AddNewTradePage : ContentPage
    {
        private readonly AddNewTradeViewModel addNewTradeViewModel;

        public AddNewTradePage()
        {
            InitializeComponent();

            addNewTradeViewModel = new AddNewTradeViewModel();
            BindingContext = addNewTradeViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            addNewTradeViewModel.Name = "";
            addNewTradeViewModel.MessageIsVisible = false;
            addNewTradeViewModel.EntryPrice = 0;
            addNewTradeViewModel.TakeProfitPrice = 0;
            addNewTradeViewModel.StopLossPrice = 0;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            addNewTradeViewModel.Name = "";
            addNewTradeViewModel.MessageIsVisible = false;
            addNewTradeViewModel.EntryPrice = 0;
            addNewTradeViewModel.TakeProfitPrice = 0;
            addNewTradeViewModel.StopLossPrice = 0;
        }

    }
}