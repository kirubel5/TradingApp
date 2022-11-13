namespace TradingApp.Models
{
    public class ArbitragePriceModel
    {
        public string ExchangeName { get; set; } = string.Empty;
        public double BidPrice { get; set; }
        public double AskPrice { get; set; }
    }
}
