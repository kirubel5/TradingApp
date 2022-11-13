using System;
using System.Collections.Generic;
using System.Text;

namespace TradingApp.Models
{
    public class BinanceTickerModel
    {
        public string symbol { get; set; } = string.Empty;
        public string bidPrice { get; set; } = string.Empty;
        public string bidQty { get; set; } = string.Empty;
        public string askPrice { get; set; } = string.Empty;
        public string askQty { get; set; } = string.Empty;
    }
}
