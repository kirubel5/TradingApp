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

        public async Task<TickerModel> GetQuote(string pair)
        {
            try
            {
                using (HttpResponseMessage response =
                    await apiConnector.ApiClient.GetAsync($"?currency_pair={pair}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsAsync<TickerModel>();
                       // var result = await response.Content.ReadAsStringAsync();
                        return result;
                    }
                    else
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Checking if Phone Number is available.");
            }
        }
    }
}
