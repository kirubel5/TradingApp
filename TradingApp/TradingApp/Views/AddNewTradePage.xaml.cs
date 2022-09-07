using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingApp.Models;
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

            if(EditableTradeModel.Id == 0)
            {
                addNewTradeViewModel.Name = "";
                addNewTradeViewModel.MessageIsVisible = false;
                addNewTradeViewModel.EntryPrice = 0;
                addNewTradeViewModel.ClosePrice = 0;
                addNewTradeViewModel.TakeProfitPrice = 0;
                addNewTradeViewModel.StopLossPrice = 0;
                addNewTradeViewModel.Amount = 0;
            }
            else
            {
                addNewTradeViewModel.Name = EditableTradeModel.Name;
                addNewTradeViewModel.MessageIsVisible = false;
                addNewTradeViewModel.EntryPrice = EditableTradeModel.EntryPrice;
                addNewTradeViewModel.ClosePrice = EditableTradeModel.ClosePrice;
                addNewTradeViewModel.TakeProfitPrice = EditableTradeModel.TakeProfitPrice;
                addNewTradeViewModel.StopLossPrice = EditableTradeModel.StopLossPrice;
                addNewTradeViewModel.Amount = EditableTradeModel.Amount;
            }
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