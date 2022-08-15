using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using TradingApp.Models;
using TradingApp.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

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
        #endregion

        public TradeViewModel()
        {
            savedTrades = new ObservableRangeCollection<TradeModel>();
            SavedTrades = new ObservableRangeCollection<TradeModel>();

            TrackCommand = new AsyncCommand(OnTrackButtonClicked);
            AddCommand = new AsyncCommand(OnAddButtonClicked);
            LeftSwipeCommand = new AsyncCommand<object>(OnLeftSwipe);
            RightSwipeCommand = new AsyncCommand<object>(OnRightSwipe);

            ShowAllTradesCommand = new AsyncCommand(OnShowAll);
            ShowDoneTradesCommand = new AsyncCommand(OnShowDone);
            ShowInProgressTradesCommand = new AsyncCommand(OnShowInProgress);
            ShowGainTradesCommand = new AsyncCommand(OnShowGain);
            ShowLossTradesCommand = new AsyncCommand(OnShowLoss);
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
            SavedTrades?.Clear();

            savedTrades = new MockData().Trades;
            SavedTrades.AddRange(savedTrades);
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

            string res = await Shell.Current.DisplayPromptAsync("Close Trade", "Insert closing price: ",
                "OK", "Cancel", "amount", 10, Keyboard.Numeric, "");


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
        }

        private async Task OnShowAll()
        {
        }

        private async Task OnShowDone()
        {
        }

        private async Task OnShowInProgress()
        {
        }

        private async Task OnShowGain()
        {
        }

        private async Task OnShowLoss()
        {
        }

        #endregion
    }
}
