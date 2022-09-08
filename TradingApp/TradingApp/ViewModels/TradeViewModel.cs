using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TradingApp.Enums;
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
        private bool isRefreshing;
        private int inProgressCount;
        private int lossCount;
        private int gainCount;
        private int totalCount;
        private double lossAmount;
        private double gainAmount;
        private double totalAmount;
        private double lossPercent;
        private double gainPercent;
        private double totalPercent;

        private List<TradeModel> savedTrades;
        public ObservableRangeCollection<TradeModel> SavedTrades { get; set; }

        private readonly IDataService _dataService;
        private readonly IValidator _validator;
        #endregion

        public TradeViewModel()
        {
            savedTrades = new List<TradeModel>();
            SavedTrades = new ObservableRangeCollection<TradeModel>();

            _dataService = new DataService();
            _validator = new Validator();

            TrackCommand = new AsyncCommand(OnTrackButtonClicked);
            AddCommand = new AsyncCommand(OnAddButtonClicked);
            LoadCommand = new AsyncCommand(Load);
            EditTradeCommand = new AsyncCommand<object>(OnEditTrade);
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
        public bool IsRefreshing
        {
            get => isRefreshing;
            set => SetProperty(ref isRefreshing, value);
        }
        public int InProgressCount
        {
            get => inProgressCount;
            set => SetProperty(ref inProgressCount, value);
        }
        public int LossCount
        {
            get => lossCount;
            set => SetProperty(ref lossCount, value);
        }
        public int GainCount
        {
            get => gainCount;
            set => SetProperty(ref gainCount, value);
        }
        public int TotalCount
        {
            get => totalCount;
            set => SetProperty(ref totalCount, value);
        }
        public double LossAmount
        {
            get => lossAmount;
            set => SetProperty(ref lossAmount, value);
        }
        public double GainAmount
        {
            get => gainAmount;
            set => SetProperty(ref gainAmount, value);
        }
        public double TotalAmount
        {
            get => totalAmount;
            set => SetProperty(ref totalAmount, value);
        }
        public double LossPercent
        {
            get => lossPercent;
            set => SetProperty(ref lossPercent, value);
        }
        public double GainPercent
        {
            get => gainPercent;
            set => SetProperty(ref gainPercent, value);
        }
        public double TotalPercent
        {
            get => totalPercent;
            set => SetProperty(ref totalPercent, value);
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
        public ICommand LoadCommand { get; }
        public ICommand EditTradeCommand { get; }
        #endregion

        #region Methods    
        public async Task Load()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            ShimmerIsActive = true;
            IsRefreshing = true;

            savedTrades.Clear();
            SavedTrades?.Clear();

            try
            {
                var (res, data) = await _dataService.GetSavedTradesAsync();

                if (!res)
                {
                    Message = "Error has occured, please try reloading the page.";
                    ImageName = "SomethingWentWrong.png";
                    return;
                }

                if(data is null || data.Count == 0)
                {
                    Message = "No Saved Trades.";
                    ImageName = "NoItem.png";
                    return;
                }

                savedTrades = Helper.FormatLoadedTrades(data);
                this.CalculateTotal();
                SavedTrades.AddRange(savedTrades);
            }
            catch (Exception) 
            {
                Message = "Error has occured, please try reloading the page.";
                ImageName = "SomethingWentWrong.png";
                return;
            }
            finally
            {
                ShimmerIsActive = false;
                IsRefreshing = false;
                IsBusy = false;
            }
        }
          
        private void CalculateTotal()
        {
            List<TradeModel> data = savedTrades.ToList();

            InProgressCount = 0;
            GainCount = 0;
            LossCount = 0;
            TotalCount = 0;

            GainAmount = 0;
            LossAmount = 0;
            TotalAmount = 0;

            GainPercent = 0;
            LossPercent = 0;
            TotalPercent = 0;

            InProgressCount = data.Where(u=>u.Status == Status.InProgress.ToString()).Count();
            GainCount = data.Where(u => u.Status == Status.Gain.ToString()).Count();
            LossCount = data.Where(u => u.Status == Status.Loss.ToString()).Count();
            TotalCount = InProgressCount + GainCount + LossCount;

            foreach (var item in data)
            {
                if (item.Status == Status.Gain.ToString())
                {
                    GainAmount += item.NetChange;
                    GainPercent += item.Percentage;
                }                    
                else if (item.Status == Status.Loss.ToString())
                {
                    LossAmount += item.NetChange;
                    LossPercent += item.Percentage;
                }                    
            }
        }

        private async Task OnEditTrade(object obj)
        {
            var val = obj as TradeModel;

            EditableTradeModel.Id = val.Id;
            EditableTradeModel.Name = val.Name;
            EditableTradeModel.Amount = val.Amount;
            EditableTradeModel.EntryPrice = val.EntryPrice;
            EditableTradeModel.ClosePrice = val.ClosePrice;
            EditableTradeModel.StopLossPrice = val.StopLossPrice;
            EditableTradeModel.TakeProfitPrice = val.TakeProfitPrice;
            EditableTradeModel.NetChange = val.NetChange;
            EditableTradeModel.Status = val.Status;
            EditableTradeModel.Percentage = val.Percentage;
            EditableTradeModel.EntryDate = val.EntryDate;
            EditableTradeModel.ExitDate = val.ExitDate;

            await Shell.Current.GoToAsync("AddNewTradePage");
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
                if (await _dataService.DeleteAsync(val))
                {
                    DependencyService.Get<IToast>()?.MakeToast("Trade Deleted Successfully");
                    await this.Load();
                }
                else
                {
                    DependencyService.Get<IToast>()?.MakeToast("Something went wrong. Please try agian");
                    return;
                }
            }
            catch (Exception)
            {
                DependencyService.Get<IToast>()?.MakeToast("Something went wrong. Please try agian");
                return;
            }
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

            if (val.Status != Status.InProgress.ToString())
                return;

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
                val.ExitDate = DateTime.Now;
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

                if (savedTrades is null || SavedTrades.Count == 0)
                {
                    Message = "No Saved Trades.";
                    ImageName = "NoItem.png";
                    return;
                }
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
                SavedTrades.AddRange(savedTrades
                    .Where(x => x.Status == Status.Gain.ToString() || x.Status == Status.Loss.ToString())
                    .ToList());

                if (SavedTrades is null || SavedTrades.Count == 0)
                {
                    Message = "No Done Trades.";
                    ImageName = "NoItem.png";
                    return;
                }
            }
            catch (Exception)
            {
                Message = "Error has occured, please try reloading the page.";
                ImageName = "SomethingWentWrong.png";
                return;
            }
        }

        public void OnShowInProgress()
        {
            try
            {
                SavedTrades?.Clear();
                SavedTrades.AddRange(savedTrades.Where(x => x.Status == Status.InProgress.ToString()).ToList());

                if (SavedTrades is null || SavedTrades.Count == 0)
                {
                    Message = "No In progress Trades.";
                    ImageName = "NoItem.png";
                    return;
                }
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
                SavedTrades.AddRange(savedTrades.Where(x => x.Status == Status.Gain.ToString()).ToList());

                if (SavedTrades is null || SavedTrades.Count == 0)
                {
                    Message = "No Gain Trades.";
                    ImageName = "NoItem.png";
                    return;
                }
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
                SavedTrades.AddRange(savedTrades.Where(x => x.Status == Status.Loss.ToString()).ToList());

                if(SavedTrades is null || SavedTrades.Count == 0)
                {
                    Message = "No Loss Trades.";
                    ImageName = "NoItem.png";
                    return;
                }
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
