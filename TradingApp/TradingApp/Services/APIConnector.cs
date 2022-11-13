using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace TradingApp.Services
{
    public class APIConnector
    {
        //Lets not make the api connector a singleton, because we want to connect to different addresses
        private static HttpClient apiClient;
        //private static APIConnector instance;

        public APIConnector(string exchangeName)
        {
            InitializeClient(exchangeName);
        }

        //public static APIConnector GetApiConnectorInstance(string exchangeName)
        //{
        //    //if (instance == null)
        //    //{
        //    //    instance = new APIConnector();
        //    //}

        //    //return instance;

        //    //everytime we get new ApiConnector, I dont know if this is a good idea
        //    return new APIConnector(exchangeName);
        //}

        public HttpClient ApiClient
        {
            get => apiClient;
        }

        private static void InitializeClient(string exchangeName)
        {
            string api = "";

            if (exchangeName == "GateIo")
                api = "https://api.gateio.ws/api/v4/spot/tickers";
            else if (exchangeName == "Binance")
                api = "https://api.binance.com/api/v3/ticker/bookTicker";
            else if (exchangeName == "OkEx")
                api = "https://www.okx.com/api/v5/market/ticker";
            else
                api = "https://api.gateio.ws/api/v4/spot/tickers"; // for safe keeping

            apiClient = new HttpClient();
            apiClient.BaseAddress = new Uri(api);
            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
