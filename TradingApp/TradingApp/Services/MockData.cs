using System;
using System.Collections.Generic;
using System.Text;
using TradingApp.Models;

namespace TradingApp.Services
{
    public class MockData
    {
        public List<TrackModel> Tracks = new List<TrackModel>()
        {
            new TrackModel {Id = 1, Name = "BTC_USDT", CurrentPrice = 23212.21, High = 23222.32, Low = 22124.52},
            new TrackModel {Id = 2, Name = "TONCOIN_USDT", CurrentPrice = 1.221, High = 1.232, Low = 1.152},
            new TrackModel {Id = 3, Name = "HIGH_USDT", CurrentPrice = 12.21, High = 12.32, Low = 11.52},
            new TrackModel {Id = 4, Name = "BTC_USDT", CurrentPrice = 23212.21, High = 23222.32, Low = 22124.52},
            new TrackModel {Id = 5, Name = "TONCOIN_USDT", CurrentPrice = 1.221, High = 1.232, Low = 1.152},
            new TrackModel {Id = 6, Name = "HIGH_USDT", CurrentPrice = 12.21, High = 12.32, Low = 11.52}
        };

        public List<TradeModel> Trades = new List<TradeModel>()
        {
            new TradeModel {Id = 1, Name = "BTC_USDT", EntryPrice = 2312.12, StopLossPrice = 2312.11, TakeProfitPrice = 2322.1, Status = "InProgress", Percentage = 2.31, EntryDate = DateTime.Now, ExitDate = DateTime.Now},
            new TradeModel {Id = 2, Name = "TONCOIN_USDT", EntryPrice = 2.12, StopLossPrice = 2.11, TakeProfitPrice = 2.1, Status = "Loss", Percentage = 0.74, EntryDate = DateTime.Now, ExitDate = DateTime.Now},
            new TradeModel {Id = 3, Name = "HIGH_USDT", EntryPrice = 23.12, StopLossPrice = 23.11, TakeProfitPrice = 23.1, Status = "Gain", Percentage = 3.26, EntryDate = DateTime.Now, ExitDate = DateTime.Now},
            new TradeModel {Id = 4, Name = "BTC_USDT", EntryPrice = 2312.12, StopLossPrice = 2312.11, TakeProfitPrice = 2322.1, Status = "InProgress", Percentage = 2.31, EntryDate = DateTime.Now, ExitDate = DateTime.Now},
            new TradeModel {Id = 5, Name = "TONCOIN_USDT", EntryPrice = 2.12, StopLossPrice = 2.11, TakeProfitPrice = 2.1, Status = "Loss", Percentage = 0.74, EntryDate = DateTime.Now, ExitDate = DateTime.Now},
            new TradeModel {Id = 6, Name = "HIGH_USDT", EntryPrice = 23.12, StopLossPrice = 23.11, TakeProfitPrice = 23.1, Status = "Gain", Percentage = 3.26, EntryDate = DateTime.Now, ExitDate = DateTime.Now}

        };

        public List<ArbitragePriceModel> ArbitragePrices = new List<ArbitragePriceModel>()
        {
            new ArbitragePriceModel {ExchangeName = "Binance", AskPrice = 20200, BidPrice=201995},
            new ArbitragePriceModel {ExchangeName = "GateIo", AskPrice = 20201, BidPrice=201994},
            new ArbitragePriceModel {ExchangeName = "OkEx", AskPrice = 20240, BidPrice=201905},
        };

    }
}
