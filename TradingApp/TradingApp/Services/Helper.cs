using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingApp.Enums;
using TradingApp.Models;

namespace TradingApp.Services
{
    public static class Helper
    {
        public static TradeModel CalculateTradeClose(TradeModel model, double closePrice)
        {
            TradeModel tradeModel = model;

            tradeModel.ClosePrice = closePrice;

            double val1 = closePrice - (tradeModel.EntryPrice * 1.00401203);

            if (val1 <= 0)            
                tradeModel.Status = Status.Loss.ToString();            
            else            
                tradeModel.Status = Status.Gain.ToString();           

            double sellOverBuy = closePrice / model.EntryPrice;
            double netChange = model.Amount * ((0.996004 * sellOverBuy) - 1);
            double percentage = (netChange / tradeModel.Amount) * 100;

            if (netChange <= 0)
                netChange = -netChange;

            if (percentage <= 0)
                percentage = -percentage;

            tradeModel.NetChange = netChange;
            tradeModel.Percentage = percentage;

            return tradeModel;
        }

        public static List<TradeModel> FormatLoadedTrades(List<TradeModel> data)
        {
            List<TradeModel> model = new List<TradeModel>();
            List<TradeModel> res = new List<TradeModel>();

            foreach (var item in data)
            {
                if(item.Status == Status.InProgress.ToString())
                {
                    if(TrackedPrice.TrackedPrices.ContainsKey(item.Name))
                    {
                        item.NetChange = (((TrackedPrice.TrackedPrices[item.Name] * 0.996004) / item.EntryPrice) - 1 ) * item.Amount;
                        item.Percentage = (item.NetChange / item.Amount) * 100;
                    }
                    else
                    {
                        item.Percentage = 0;
                        item.NetChange = 0;
                    }

                    item.ExitDate = DateTime.Now;
                }

                model.Add(item);
            }

            res.AddRange(model.OrderByDescending(x => x.EntryDate));

            return res;
        }

        public static async Task<List<TrackModel>> LoadTrackInformation(List<TrackModel> data)
        {
            List<TrackModel> model = new List<TrackModel>();
            TrackedPrice.TrackedPrices.Clear();

            foreach (var item in data)
            {
                var (_, result) = await Endpoints.GetGateIoTicker(item.Name);
               
                item.CurrentPrice = double.Parse(result.last);
                item.High = double.Parse(result.high_24h);
                item.Low = double.Parse(result.low_24h);
                item.Percentage = double.Parse(result.change_percentage);

                try
                {
                    TrackedPrice.TrackedPrices.Add(item.Name, item.CurrentPrice);
                }
                catch (Exception)
                {
                    //Dont do anything, just move on
                }

                model.Add(item);
            }

            return FormatLoadedTracks(model.OrderByDescending(u => u.Id).ToList());
        }

        private static List<TrackModel> FormatLoadedTracks(List<TrackModel> data)
        {
            foreach (var item in data)
            {
                if (item.Percentage < 0)
                {
                    item.Status = Status.Loss.ToString();
                    item.Percentage = -1 * item.Percentage;
                }                    
                else
                    item.Status = Status.Gain.ToString();
            }

            return data;
        }

        public static async Task<bool> CheckQuote(string name)
        {           
            var (result, _) = await Endpoints.GetGateIoTicker(name);

            if (!result)
                return false;
            else
                return true;           
        }

        public static async Task<IEnumerable<ArbitragePriceModel>> LoadArbitrageInformation(string quoteName, string baseName)
        {
            List<ArbitragePriceModel> data = new List<ArbitragePriceModel>();

            try
            {
                var (result1, value1) = await Endpoints.GetGateIoTicker(baseName + "_" + quoteName);

                if(result1 && value1 != null )
                {
                    data.Add(new ArbitragePriceModel
                    {
                        ExchangeName = "GateIo",
                        BidPrice = double.Parse(value1.highest_bid),
                        AskPrice = double.Parse(value1.lowest_ask)
                    });
                }
                else
                {
                    data.Add(new ArbitragePriceModel
                    {
                        ExchangeName = "GateIo",
                        BidPrice = 0,
                        AskPrice = 0
                    });
                }

                var (result2, value2) = await Endpoints.GetBinanceTicker(baseName + quoteName);
                
                if(result2 && value2 != null)
                {
                    data.Add(new ArbitragePriceModel
                    {
                        ExchangeName = "Binance",
                        BidPrice = double.Parse(value2.bidPrice),
                        AskPrice = double.Parse(value2.askPrice)
                    });
                }
                else
                {
                    data.Add(new ArbitragePriceModel
                    {
                        ExchangeName = "Binance",
                        BidPrice = 0,
                        AskPrice = 0
                    });
                }

                var (result3, value3) = await Endpoints.GetOkExTicker(baseName + "-" + quoteName + "-SWAP");

                return data;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
