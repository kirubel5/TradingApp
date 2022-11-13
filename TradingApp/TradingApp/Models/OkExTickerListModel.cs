using System;
using System.Collections.Generic;
using System.Text;

namespace TradingApp.Models
{
    public class OkExTickerListModel
    {
        public string Code { get; set; }
        public string Msg { get; set; }
        public List<OkExTickerModel> Data { get; set; }
    }
}
