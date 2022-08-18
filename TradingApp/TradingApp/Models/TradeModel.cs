using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TradingApp.Models
{
    public class TradeModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double EntryPrice { get; set; }
        public double StopLossPrice { get; set; }
        public double TakeProfitPrice { get; set; }
        public double Amount { get; set; }
        public double NetChange { get; set; }
        public string Status { get; set; } = string.Empty;
        public double Percentage { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ExitDate { get; set; }
    }
}
