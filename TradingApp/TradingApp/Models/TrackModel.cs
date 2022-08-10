using System;
using System.Collections.Generic;
using System.Text;

namespace TradingApp.Models
{
    public class TrackModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double CurrentPrice { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
    }
}
