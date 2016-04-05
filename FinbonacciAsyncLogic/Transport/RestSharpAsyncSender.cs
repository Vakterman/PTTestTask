using System;
using FinbonacciAsyncLogic.Interfaces;
using FinbonacciAsyncLogic.Entities;
using RestSharp;

namespace FinbonacciAsyncLogic.Transport
{
    public class RestSharpAsyncSender : IAsyncSender<FibonacciOperation>
    {
        IConfigurationManager _configurationManager;
        public RestSharpAsyncSender(IConfigurationManager configurationManager) {
            if (configurationManager == null)
            {
                throw new ArgumentNullException("configurationManager");
            }

            _configurationManager = configurationManager;
        }

        public void Dispose()
        {
            //There are no time critical resources
        }

        public void SendAsync(FibonacciOperation objectForSend)
        {
            var client = new RestClient(_configurationManager.WebApiServerAddress);
            var request = new RestRequest("api/fibonacci", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(objectForSend);

            IRestResponse response = client.Execute(request);
        }
    }
}
