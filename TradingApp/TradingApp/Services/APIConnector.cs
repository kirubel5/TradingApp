using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace TradingApp.Services
{
    public class APIConnector
    {
        private static HttpClient apiClient;
        private static APIConnector instance;
        private APIConnector()
        {
            InitializeClient();
        }

        public static APIConnector GetApiConnectorInstance()
        {
            if (instance == null)
            {
                instance = new APIConnector();
            }

            return instance;
        }

        public HttpClient ApiClient
        {
            get => apiClient;
        }

        private static void InitializeClient()
        {
            string api = "https://api.gateio.ws/api/v4/spot/tickers";

            apiClient = new HttpClient();
            apiClient.BaseAddress = new Uri(api);
            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
