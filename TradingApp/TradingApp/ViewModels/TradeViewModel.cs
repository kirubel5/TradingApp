using MvvmHelpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using TradingApp.Models;
using TradingApp.Services;

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
        #endregion

        #region Methods    
        public async Task Load()
        {
            SavedTrades?.Clear();

            savedTrades = new MockData().Trades;
            SavedTrades.AddRange(savedTrades);
        }

        #endregion
    }
}
