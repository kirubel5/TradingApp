﻿using System;
using System.Collections.Generic;
using System.Text;
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

            tradeModel.Percentage = val3;
            tradeModel.ExitDate = DateTime.Now;

            return tradeModel;
        }

        public static List<TradeModel> FormatLoadedTrades(List<TradeModel> data)
        {
            List<TradeModel> model = new List<TradeModel>();

            foreach (var item in data)
            {
                if(item.Status == "In Progress")
                {
                    if(TrackedPrice.TrackedPrices.ContainsKey(item.Name))
                    {
                        item.Percentage = ((item.EntryPrice - TrackedPrice.TrackedPrices[item.Name]) / item.EntryPrice) * 100;
                    }
                    else
                    {
                        item.Percentage = 0;
                    }

                    item.ExitDate = DateTime.MaxValue;
                }

                model.Add(item);
            }

            return model;
        }
    }
}