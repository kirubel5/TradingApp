using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TradingApp.Models;

namespace TradingApp.Services
{
    public class TradingDatabase
    {
        readonly SQLiteAsyncConnection database;

        public TradingDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);

            database.CreateTableAsync<TradeModel>().Wait();
           // database.CreateTableAsync<TrackModel>().Wait();
        }

        public Task<int> SaveTradeAsync(TradeModel model)
        => database.InsertAsync(model);

        public Task<int> SaveTrackAsync(TrackModel model)
        => database.InsertAsync(model);

        public Task<List<TradeModel>> GetSavedTradesAsync()
        => database.Table<TradeModel>().ToListAsync();

        public Task<List<TrackModel>> GetSavedTracksAsync() 
        => database.Table<TrackModel>().ToListAsync();

        public Task<int> DeleteTradeAsync(TradeModel model) 
        => database.DeleteAsync(model);

        public Task<int> DeleteTrackAsync(TrackModel model) 
        => database.DeleteAsync(model);

        public Task<int> UpdateTradeAsync(TradeModel model)
        => database.UpdateAsync(model);

    }
}
