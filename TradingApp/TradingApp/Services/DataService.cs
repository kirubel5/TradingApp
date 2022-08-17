using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TradingApp.Models;

namespace TradingApp.Services
{
    public class DataService : IDataService
    {
        public async Task<bool> CreateAsync(dynamic model)
        {
            try
            {
                if (model is TrackModel trackModel)
                {
                    if (await App.Database.SaveTrackAsync(trackModel) != 0)
                        return true;
                }
                else if (model is TradeModel tradeModel)
                {
                    if (await App.Database.SaveTradeAsync(tradeModel) != 0)
                        return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        public async Task<(bool, List<TradeModel>)> GetSavedTradesAsync()
        {
            try
            {
                return (true, await App.Database.GetSavedTradesAsync());
            }
            catch (Exception)
            {
                return (false, null);
            }
        }

        public async Task<(bool, List<TrackModel>)> GetSavedTracksAsync()
        {
            try
            {
                return (true, await App.Database.GetSavedTracksAsync());
            }
            catch (Exception)
            {
                return (false, null);
            }
        }

        public async Task<bool> DeleteAsync(dynamic model)
        {
            try
            {
                if (model is TrackModel trackModel)
                {
                    if (await App.Database.DeleteTrackAsync(trackModel) != 0)
                        return true;
                }
                else if (model is TradeModel tradeModel)
                {
                    if (await App.Database.DeleteTradeAsync(tradeModel) != 0)
                        return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        public async Task<bool> UpdateAsync(TradeModel model)
        {
            try
            {
                if (await App.Database.UpdateTradeAsync(model) != 0)
                    return true;
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }
    }
}
