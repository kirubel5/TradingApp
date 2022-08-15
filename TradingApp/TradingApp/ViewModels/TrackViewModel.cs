using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TradingApp.Models;
using TradingApp.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TradingApp.ViewModels
{
    public class TrackViewModel : ViewModelBase
    {
        #region Fields
        private string imageName;
        private string message;

        private IEnumerable<TrackModel> trackedTrades;
        public ObservableRangeCollection<TrackModel> TrackedTrades { get; set; }
        #endregion

        public TrackViewModel()
        {
            trackedTrades = new ObservableRangeCollection<TrackModel>();
            TrackedTrades = new ObservableRangeCollection<TrackModel>();

            AddCommand = new AsyncCommand(OnAddButtonClicked);
            TradeCommand = new AsyncCommand(OnTradeButtonClicked);
            RightSwipeCommand = new AsyncCommand<object>(OnRightSwipe);
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
        #endregion

        #region Commands

        public ICommand AddCommand { get; }
        public ICommand TradeCommand { get; }
        public ICommand RightSwipeCommand { get; }
        #endregion

        #region Methods
        public async Task Load()
        {
            TrackedTrades?.Clear();

            trackedTrades = new MockData().Tracks;
            TrackedTrades.AddRange(trackedTrades);
        }

        private async Task OnAddButtonClicked()
        {
            await Shell.Current.GoToAsync("AddNewTrackPage");
        }

        private async Task OnTradeButtonClicked()
        {
            await Shell.Current.GoToAsync("//TradePage");
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
        #endregion
    }
}
