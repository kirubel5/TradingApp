using System;

namespace TradingApp.Models
{
    public static class EditableTradeModel
    {
        public static int Id { get; set; }
        public static string Name { get; set; } = string.Empty;
        public static double EntryPrice { get; set; }
        public static double ClosePrice { get; set; }
        public static double StopLossPrice { get; set; }
        public static double TakeProfitPrice { get; set; }
        public static double Amount { get; set; }
        public static double NetChange { get; set; }
        public static string Status { get; set; }
        public static double Percentage { get; set; }
        public static DateTime EntryDate { get; set; }
        public static DateTime ExitDate { get; set; }
    }
}
