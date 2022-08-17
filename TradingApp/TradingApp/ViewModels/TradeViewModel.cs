using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TradingApp.Models;
using TradingApp.Services;
using WaissLibraryStandard;
using Xamarin.Essentials;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;

namespace TradingApp.ViewModels
{
    public class TradeViewModel : ViewModelBase
    {
        #region Fields
        private bool shimmerIsActive;
        private string imageName;
        private string message;

        private IEnumerable<TradeModel> savedTrades;
        public ObservableRangeCollection<TradeModel> SavedTrades { get; set; }

        private readonly IDataService _dataService;
        private readonly IValidator _validator;
        #endregion

        public TradeViewModel()
        {
            savedTrades = new ObservableRangeCollection<TradeModel>();
            SavedTrades = new ObservableRangeCollection<TradeModel>();

            _dataService = new DataService();
            _validator = new Validator();

            TrackCommand = new AsyncCommand(OnTrackButtonClicked);
            AddCommand = new AsyncCommand(OnAddButtonClicked);
            LeftSwipeCommand = new AsyncCommand<object>(OnLeftSwipe);
            RightSwipeCommand = new AsyncCommand<object>(OnRightSwipe);
            ShowAllTradesCommand = new Command(OnShowAll);
            ShowDoneTradesCommand = new Command(OnShowDone);
            ShowInProgressTradesCommand = new Command(OnShowInProgress);
            ShowGainTradesCommand = new Command(OnShowGain);
            ShowLossTradesCommand = new Command(OnShowLoss);
        }

        #region Properties
        public string ImageName
        {
            get => imageName;
            set => SetProperty(ref imageName, value);
        }
        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }
        public bool ShimmerIsActive
        {
            get => shimmerIsActive;
            set => SetProperty(ref shimmerIsActive, value);
        }

        #endregion

        #region Commands
        public ICommand ShowAllTradesCommand { get; }
        public ICommand ShowDoneTradesCommand { get; }
        public ICommand ShowInProgressTradesCommand { get; }
        public ICommand ShowGainTradesCommand { get; }
        public ICommand ShowLossTradesCommand { get; }
        public ICommand LeftSwipeCommand { get; }
        public ICommand RightSwipeCommand { get; }
        public ICommand TrackCommand { get; }
        public ICommand AddCommand { get; }
        #endregion

        #region Methods    
        public async Task Load()
        {
            ShimmerIsActive = true;
            SavedTrades?.Clear();

            try
            {
                var (res,data) = await _dataService.GetSavedTradesAsync();

                if (!res)
                {
                    ShimmerIsActive = false;
                    Message = "Error has occured, please try reloading the page.";
                    ImageName = "SomethingWentWrong.png";
                    return;
                }

                if(data is null || data.Count == 0)
                {
                    ShimmerIsActive = false;
                    Message = "No Saved Trades.";
                    ImageName = "NoItem.png";
                    return;
                }

                ShimmerIsActive = false;
                savedTrades = Helper.FormatLoadedTrades(data);

                SavedTrades?.AddRange(savedTrades);
            }
            catch (Exception)
            {
                ShimmerIsActive = false;
                Message = "Error has occured, please try reloading the page.";
                ImageName = "SomethingWentWrong.png";
                return;
            }
        }        

        private async Task OnTrackButtonClicked()
        {
            await Shell.Current.GoToAsync("//TrackPage");
        }

        private async Task OnAddButtonClicked()
        {
            await Shell.Current.GoToAsync("AddNewTradePage");
        }

        private async Task OnLeftSwipe(object obj)
        {
            try
            {
                var duration = TimeSpan.FromMilliseconds(100);
                Vibration.Vibrate(duration);
            }
            catch (Exception)
            {
                // Dont do any thing, just move on
            }

            var val = obj as TradeModel;

            bool res = await Shell.Current.DisplayAlert("Delete Trade", $"Are you sure you want to detele " +
                $"trade: {val.Name}?", "Ok", "Cancel");

            if (!res)
                return;

            try
            {
                await _dataService.DeleteAsync(val);
            }
            catch (Exception)
            {
                DependencyService.Get<IToast>()?.MakeToast("Something went wrong. Please try agian.");
                return;
            }

            DependencyService.Get<IToast>()?.MakeToast("Trade Deleted Successfully.");

            await this.Load();
        }

        private async Task OnRightSwipe(object obj)
        {
            try
            {
                var duration = TimeSpan.FromMilliseconds(100);
                Vibration.Vibrate(duration);
            }
            catch (Exception)
            {
                // Dont do any thing, just move on
            }

            var val = obj as TradeModel;

            string res = await Shell.Current.DisplayPromptAsync("Close Trade", "Please insert the price at which the " +
                "trade is closing: ", "Ok", "Cancel", "amount", 20, Keyboard.Numeric, "");

            if (string.IsNullOrWhiteSpace(res))
                return;

            if (!_validator.IsDouble(res))
            {
                await Shell.Current.DisplayAlert("", "Closing price has to be a number", "Ok");
                return;
            }

            try
            {
                await _dataService.UpdateAsync(Helper.CalculateTradeClose(val, double.Parse(res)));
            }
            catch (Exception)
            {
                DependencyService.Get<IToast>()?.MakeToast("Something went wrong. Please try agian.");
                return;
            }

            DependencyService.Get<IToast>()?.MakeToast("Trade Closed Successfully.");

            await this.Load();
        }

        private void OnShowAll()
        {
            try
            {
                SavedTrades?.Clear();
                SavedTrades.AddRange(savedTrades);
            }
            catch (Exception)
            {
                Message = "Error has occured, please try reloading the page.";
                ImageName = "SomethingWentWrong.png";
                return;
            }
        }

        private void OnShowDone()
        {
            try
            {
                SavedTrades?.Clear();
                SavedTrades.AddRange(savedTrades.Where(x => x.Status == "Gain" || x.Status == "Loss").ToList());
            }
            catch (Exception)
            {
                Message = "Error has occured, please try reloading the page.";
                ImageName = "SomethingWentWrong.png";
                return;
            }
        }

        private void OnShowInProgress()
        {
            try
            {
                SavedTrades?.Clear();
                SavedTrades.AddRange(savedTrades.Where(x => x.Status == "In Progress").ToList());
            }
            catch (Exception)
            {
                Message = "Error has occured, please try reloading the page.";
                ImageName = "SomethingWentWrong.png";
                return;
            }
        }

        private void OnShowGain()
        {
            try
            {
                SavedTrades?.Clear();
                SavedTrades.AddRange(savedTrades.Where(x => x.Status == "Gain").ToList());
            }
            catch (Exception)
            {
                Message = "Error has occured, please try reloading the page.";
                ImageName = "SomethingWentWrong.png";
                return;
            }
        }

        private void OnShowLoss()
        {
            try
            {
                SavedTrades?.Clear();
                SavedTrades.AddRange(savedTrades.Where(x => x.Status == "Loss").ToList());
            }
            catch (Exception)
            {
                Message = "Error has occured, please try reloading the page.";
                ImageName = "SomethingWentWrong.png";
                return;
            }
        }

        #endregion
    }
}
