
using MvvmHelpers.Commands;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using TradingApp.Models;
using TradingApp.Services;
using Xamarin.Forms;

namespace TradingApp.ViewModels
{
    public class AddNewTradeViewModel : ViewModelBase
    {
        #region Fields
        private bool messageIsVisible;
        private string message;
        private string name;
        private double entryPrice;
        private double takeProfitPrice;
        private double stopLossPrice;
        private double amount;

        private readonly IDataService _dataService;
        #endregion

        public AddNewTradeViewModel()
        {
            BackCommand = new AsyncCommand(OnBackButtonClicked);
            SaveCommand = new AsyncCommand(OnSaveButtonClicked);

            _dataService = new DataService();
        }

        #region Properties
        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }

        public bool MessageIsVisible
        {
            get => messageIsVisible;
            set => SetProperty(ref messageIsVisible, value);
        }
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        public double EntryPrice
        {
            get => entryPrice;
            set => SetProperty(ref entryPrice, value);
        }
        public double TakeProfitPrice
        {
            get => takeProfitPrice;
            set => SetProperty(ref takeProfitPrice, value);
        }
        public double StopLossPrice
        {
            get => stopLossPrice;
            set => SetProperty(ref stopLossPrice, value);
        }
        public double Amount
        {
            get => amount;
            set => SetProperty(ref amount, value);
        }

        #endregion

        #region Commands
        public ICommand BackCommand { get; }
        public ICommand SaveCommand { get; }
        #endregion

        #region Methods

        private async Task OnSaveButtonClicked()
        {
            try
            {
                MessageIsVisible = false;

                #region Input validation

                if (string.IsNullOrWhiteSpace(Name))
                {
                    Message = "Name cannot be empty.";
                    MessageIsVisible = true;
                    return;
                }

                if (string.IsNullOrWhiteSpace(EntryPrice.ToString()) || EntryPrice == 0)
                {
                    Message = "Entry Price cannot be empty or zero.";
                    MessageIsVisible = true;
                    return;
                }

                if (string.IsNullOrWhiteSpace(StopLossPrice.ToString()) || StopLossPrice == 0)
                {
                    Message = "Stop Loss Price cannot be empty or zero.";
                    MessageIsVisible = true;
                    return;
                }

                if (string.IsNullOrWhiteSpace(TakeProfitPrice.ToString()) || TakeProfitPrice == 0)
                {
                    Message = "Take Profit Price cannot be empty or zero.";
                    MessageIsVisible = true;
                    return;
                }

                if (string.IsNullOrWhiteSpace(Amount.ToString()) || Amount == 0)
                {
                    Message = "Amount cannot be empty or zero.";
                    MessageIsVisible = true;
                    return;
                }
                #endregion

                TradeModel model = new TradeModel()
                {
                    Name = Name,
                    EntryPrice = EntryPrice,
                    Status = "In Progress",
                    TakeProfitPrice = TakeProfitPrice,
                    StopLossPrice = StopLossPrice,
                    Amount = Amount,
                    EntryDate = DateTime.Now
                };

                IsBusy = true;

                if (await _dataService.ExistsAsync(model))
                {
                    Message = "Quote already saved in database";
                    IsBusy = false;
                    return;
                }

                if (await _dataService.CreateAsync(model))
                {
                    DependencyService.Get<IToast>()?.MakeToast("Saved Successfully");
                    IsBusy = false;
                    Name = "";
                    EntryPrice = 0;
                    TakeProfitPrice = 0;
                    StopLossPrice = 0;
                    Amount = 0;
                }
                else
                {
                    IsBusy = false;
                    return;
                }
            }
            catch (Exception)
            {
                Message = "Something went wrong, please try again";
                MessageIsVisible = true;
            }
        }

        private async Task OnBackButtonClicked()
        {
            await Shell.Current.GoToAsync("..");
        }        
        #endregion
    }
}
