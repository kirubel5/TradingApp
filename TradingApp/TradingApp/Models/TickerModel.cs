using System;
using System.Collections.Generic;
using System.Text;

namespace TradingApp.Models
{
    public class TickerModel
    {
        //not following naming convention to match API
        public string currency_pair { get; set; }
        public string last { get; set; }
        public string lowest_ask { get; set; }
        public string highest_bid { get; set; }
        public string change_percentage { get; set; }
        public string base_volume { get; set; }
        public string quote_volume { get; set; }
        public string high_24h { get; set; }
        public string low_24h { get; set; }
    }
}
