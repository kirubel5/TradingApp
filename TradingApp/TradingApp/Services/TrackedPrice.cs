using System;
using System.Collections.Generic;
using System.Text;

namespace TradingApp.Services
{
    public static class TrackedPrice
    {
        public static Dictionary<string, double> TrackedPrices { get; set; } = new Dictionary<string, double>();
    }
}
