using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TradingApp.Models;

namespace TradingApp.Services
{
    public class Endpoints
    {
        private readonly APIConnector apiConnector = APIConnector.GetApiConnectorInstance();

        public async Task<(bool, TickerModel)> GetQuote(string pair)
        {
            try
            {
                using (HttpResponseMessage response =
                    await apiConnector.ApiClient.GetAsync($"?currency_pair={pair}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsAsync<List<TickerModel>>();
                        return (true, result[0]);
                    }
                    else
                    {
                        return (false, null);
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Error getting quote");
            }
        }
    }
}
