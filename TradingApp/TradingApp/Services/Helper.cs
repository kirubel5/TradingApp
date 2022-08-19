using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingApp.Models;

namespace TradingApp.Services
{
    public static class Helper
    {
        public static TradeModel CalculateTradeClose(TradeModel model, double closePrice)
        {
            TradeModel tradeModel = model;

            double val1 = closePrice - tradeModel.EntryPrice;

            if (val1 <= 0)
            {
                val1 = -(val1);
                tradeModel.Status = "Loss";
            }
            else
            {
                tradeModel.Status = "Gain";
            }

            double val2 = val1 / tradeModel.EntryPrice;
            double val3 = val2 * 100;

            tradeModel.NetChange = model.Amount * val2;
            tradeModel.Percentage = val3;
            tradeModel.ExitDate = DateTime.Now;

            return tradeModel;
        }

        public static List<TradeModel> FormatLoadedTrades(List<TradeModel> data)
        {
            List<TradeModel> model = new List<TradeModel>();
            List<TradeModel> res = new List<TradeModel>();

            foreach (var item in data)
            {
                if(item.Status == "In Progress")
                {
                    if(TrackedPrice.TrackedPrices.ContainsKey(item.Name))
                    {
                        item.Percentage = ((item.EntryPrice - TrackedPrice.TrackedPrices[item.Name]) / item.EntryPrice) * 100;
                        item.NetChange = ((item.EntryPrice - TrackedPrice.TrackedPrices[item.Name]) / item.EntryPrice) * item.Amount;
                    }
                    else
                    {
                        item.Percentage = 0;
                        item.NetChange = 0;
                    }

                    item.ExitDate = DateTime.MaxValue;
                }

                model.Add(item);
            }

            res.AddRange(model.OrderByDescending(x => x.EntryDate));

            return res;
        }

        public static async Task<List<TrackModel>> LoadTrackInformation(List<TrackModel> data)
        {
            List<TrackModel> model = new List<TrackModel>();

            foreach (var item in data)
            {
                var (_, result) = await new Endpoints().GetQuote(item.Name);
                TrackModel resModel = new TrackModel
                {
                    Name = item.Name,
                    CurrentPrice = double.Parse(result.last),
                    High = double.Parse(result.high_24h),
                    Low = double.Parse(result.low_24h),
                    Percentage = double.Parse(result.change_percentage)
                };

                model.Add(resModel);
            }

            return FormatLoadedTracks(model);
        }

        private static List<TrackModel> FormatLoadedTracks(List<TrackModel> data)
        {
            foreach (var item in data)
            {
                if (item.Percentage < 0)
                {
                    item.Status = "Loss";
                    item.Percentage = -1 * item.Percentage;
                }                    
                else
                    item.Status = "Gain";
            }

            return data;
        }

        public static async Task<bool> CheckQuote(string name)
        {           
            var (result, _) = await new Endpoints().GetQuote(name);

            if (!result)
                return false;
            else
                return true;           
        }
    }
}
