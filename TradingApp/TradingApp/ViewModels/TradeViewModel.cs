using MvvmHelpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradingApp.Models;
using TradingApp.Services;

namespace TradingApp.ViewModels
{
    public class TradeViewModel : ViewModelBase
    {
        #region Fields
        private bool shimmerIsActive;
        private IEnumerable<TradeModel> savedTrades;

        public ObservableRangeCollection<TradeModel> SavedTrades { get; set; }
        #endregion

        public TradeViewModel()
        {
            savedTrades = new ObservableRangeCollection<TradeModel>();
            SavedTrades = new ObservableRangeCollection<TradeModel>();
        }

        #region Properties
        public bool ShimmerIsActive
        {
            get => shimmerIsActive;
            set => SetProperty(ref shimmerIsActive, value);
        }

        #endregion

        #region Commands
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
