using System;
using System.Collections.Generic;
using TradingApp.Enums;
using TradingApp.Models;

namespace TradingApp.Services
{
    public static class Seed
    {
        public static void SeedTrade()
        {
            List<TradeModel> trade = new List<TradeModel>
            {
                new TradeModel
                {
                    Name = "ATM_USDT",
                    EntryPrice = 3.82,
                    ClosePrice = 4.98,
                    StopLossPrice = 3.66,
                    TakeProfitPrice = 4.994,
                    Amount = 10.085,
                    EntryDate = DateTime.Parse("07/23/2022 20:30"),
                    ExitDate = DateTime.Parse("07/30/2022 01:25")
                },

                new TradeModel
                {
                    Name = "POND_USDT",
                    EntryPrice = 0.01498,
                    ClosePrice = 0.01413,
                    StopLossPrice = 0.01414,
                    TakeProfitPrice = 0.01817,
                    Amount = 10,
                    EntryDate = DateTime.Parse("07/31/2022 15:18"),
                    ExitDate = DateTime.Parse("08/02/2022 01:25")
                },

                new TradeModel
                {
                    Name = "HIGH_USDT",
                    EntryPrice = 1.925,
                    ClosePrice = 1.9262,
                    StopLossPrice = 1.775,
                    TakeProfitPrice = 2.51,
                    Amount = 9.981,
                    EntryDate = DateTime.Parse("08/02/2022 20:16"),
                    ExitDate = DateTime.Parse("08/10/2022 14:34")
                },

                new TradeModel
                {
                   Name = "TCT_USDT",
                    EntryPrice = 0.012683,
                    ClosePrice = 0.01126,
                    StopLossPrice = 0.01127,
                    TakeProfitPrice = 0.01578,
                    Amount = 10.1,
                    EntryDate = DateTime.Parse("08/16/2022 15:57"),
                    ExitDate = DateTime.Parse("08/19/2022 08:11")
                },

                 new TradeModel
                 {
                    Name = "DEXE_USDT",
                    EntryPrice = 3.14,
                    ClosePrice = 3.06,
                    StopLossPrice = 2.98,
                    TakeProfitPrice = 3.768,
                    Amount = 9.087,
                    EntryDate = DateTime.Parse("08/20/2022 18:14"),
                    ExitDate = DateTime.Parse("08/23/2022 00:23")
                 },

                 new TradeModel
                 {
                    Name = "TONCOIN_USDT",
                    EntryPrice = 1.2674,
                    ClosePrice = 1.511,
                    StopLossPrice = 1.197,
                    TakeProfitPrice = 1.512,
                    Amount = 8.886,
                    EntryDate = DateTime.Parse("08/23/2022 00:28"),
                    ExitDate = DateTime.Parse("08/24/2022 03:56")
                 },

                 new TradeModel
                 {
                    Name = "NBS_USDT",
                    EntryPrice = 0.00466,
                    ClosePrice = 0.004434,
                    StopLossPrice = 0.004434,
                    TakeProfitPrice = 0.0056,
                    Amount = 10,
                    EntryDate = DateTime.Parse("08/25/2022 14:38"),
                    ExitDate = DateTime.Parse("08/26/2022 13:41")
                 },

                 new TradeModel
                 {
                    Name = "ASR_USDT",
                    EntryPrice = 4.32,
                    ClosePrice = 4.57,
                    StopLossPrice = 4.01,
                    TakeProfitPrice = 4.988,
                    Amount = 9.478,
                    EntryDate = DateTime.Parse("08/27/2022 20:38"),
                    ExitDate = DateTime.Parse("09/02/2022 15:55")
                 },

                 new TradeModel
                 {
                    Name = "MDT_USDT",
                    EntryPrice = 0.02575,
                    ClosePrice = 0.02302,
                    StopLossPrice = 0.02446,
                    TakeProfitPrice = 0.0309,
                    Amount = 10,
                    EntryDate = DateTime.Parse("09/02/2022 16:19"),
                    ExitDate = DateTime.Parse("09/07/2022 15:42")
                 },

                 new TradeModel
                 {
                    Name = "DATA_USDT",
                    EntryPrice = 0.029,
                    ClosePrice = 0.0348,
                    StopLossPrice = 0.02775,
                    TakeProfitPrice = 0.0348,
                    Amount = 9,
                    EntryDate = DateTime.Parse("09/09/2022 11:13"),
                    ExitDate = DateTime.Parse("09/12/2022 14:44")
                 },
            };

            IDataService service = new DataService();

            foreach (var item in trade)
            {
                try
                {
                    service.CreateAsync(Helper.CalculateTradeClose(item, item.ClosePrice));
                }
                catch (Exception)
                {
                    try
                    {
                        service.CreateAsync(Helper.CalculateTradeClose(item, item.ClosePrice));
                    }
                    catch (Exception)
                    {
                        //dont do anything
                    }
                }
            }
        }
    }
}
