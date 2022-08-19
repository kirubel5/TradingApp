using SQLite;

namespace TradingApp.Models
{
    public class TrackModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public double CurrentPrice { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Percentage { get; set; }
    }
}
