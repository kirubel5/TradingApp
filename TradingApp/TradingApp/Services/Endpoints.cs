using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TradingApp.Models;

namespace TradingApp.Services
{
    public static class Endpoints
    {
        public static async Task<(bool, TickerModel)> GetGateIoTicker(string pair)
        {
            APIConnector apiConnector = new APIConnector("GateIo");

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
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return (false, null);
            }
        }

        public static async Task<(bool, BinanceTickerModel)> GetBinanceTicker(string pair)
        {
            APIConnector apiConnector = new APIConnector("Binance");

            try
            {
                using (HttpResponseMessage response =
                    await apiConnector.ApiClient.GetAsync($"?symbol={pair}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsAsync<BinanceTickerModel>();
                        return (true, result);
                    }
                    else
                    {
                        return (false, null);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return (false, null);
            }
        }

        public static async Task<(bool, OkExTickerModel)> GetOkExTicker(string pair)
        {
            APIConnector apiConnector = new APIConnector("OkEx");

            try
            {
                using (HttpResponseMessage response =
                    await apiConnector.ApiClient.GetAsync($"?instId={pair}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsAsync<OkExTickerListModel>();
                        return (true, result.Data[0]);
                    }
                    else
                    {
                        return (false, null);
                    }
                }
            }
            catch (Exception ex) 
            {
                Debug.Print(ex.Message);
                return (false, null);
            }
        }
    }
}
