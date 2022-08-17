
using MvvmHelpers.Commands;
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

        #endregion

        #region Commands
        public ICommand BackCommand { get; }
        public ICommand SaveCommand { get; }
        #endregion

        #region Methods

        private async Task OnSaveButtonClicked()
        {
            MessageIsVisible = false;

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

            MessageIsVisible = false;

            TradeModel model = new TradeModel()
            {
                Name = Name,
                EntryPrice = EntryPrice,
                TakeProfitPrice = TakeProfitPrice,
                StopLossPrice = StopLossPrice
            };

            IsBusy = true;
            //save to database. instead of talking to the database directly, talk to a class
            //that talks to the database class
            await _dataService.CreateAsync(model);


            IsBusy = false;

            Name = "";
        }

        private async Task OnBackButtonClicked()
        {
            await Shell.Current.GoToAsync("..");
        }        
        #endregion
    }
}
