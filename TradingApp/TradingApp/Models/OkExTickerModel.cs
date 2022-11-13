using System;
using System.Collections.Generic;
using System.Text;

namespace TradingApp.Models
{
    public class OkExTickerModel
    {        
        public string InstType { get; set; }
        public string InstId { get; set; }
        public string Last { get; set; }
        public string LastSz { get; set; }
        public string AskPx { get; set; }
        public string AskSz { get; set; }
        public string BidPx { get; set; }
        public string BidSz { get; set; }
        public string Open24h { get; set; }
        public string High24h { get; set; }
        public string Low24h { get; set; }
        public string VolCcy24h { get; set; }
        public string Vol24h { get; set; }
        public string Ts { get; set; }
        public string SodUtc0 { get; set; }
        public string SodUtc8 { get; set; }
       
    }
}
