using System.Collections.Generic;
using System.Threading.Tasks;
using TradingApp.Models;

namespace TradingApp.Services
{
    public interface IDataService
    {
        Task<bool> CreateAsync(dynamic model);
        Task<bool> ExistsAsync(dynamic model);
        Task<bool> DeleteAsync(dynamic model);
        Task<(bool, List<TrackModel>)> GetSavedTracksAsync();
        Task<(bool, List<TradeModel>)> GetSavedTradesAsync();
        Task<bool> UpdateAsync(TradeModel model);
    }
}