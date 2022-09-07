﻿using System;
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

            tradeModel.ClosePrice = closePrice;

            double val1 = closePrice - (tradeModel.EntryPrice * 1.00401203);

            if (val1 <= 0)            
                tradeModel.Status = "Loss";            
            else            
                tradeModel.Status = "Gain";            

            double sellOverBuy = closePrice / model.EntryPrice;
            double netChange = model.Amount * ((0.996004 * sellOverBuy) - 1);
            double percentage = (tradeModel.NetChange / tradeModel.Amount) * 100;

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
                if(item.Status == "In Progress")
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
                var (_, result) = await new Endpoints().GetQuote(item.Name);
               
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
