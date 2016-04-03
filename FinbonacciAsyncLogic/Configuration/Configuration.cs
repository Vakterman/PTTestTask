using FinbonacciAsyncLogic.Interfaces;
using System.Configuration;
using System;

namespace FinbonacciAsyncLogic.Configuration
{
    public class Configuration : IConfigurationManager
    {
        private const string _QueueFibonacciName = "FibonacciQueueName";
        private const string _UserName = "UserName";
        private const string _PasswordName = "Password";
        private const string _QueueAdress = "QueueFibonacciAdress";
        private const string _WebApiServerAddressName = "WebApiServerAddressName";
        private const string _DefaultTimeoutParamName = "DefaultTimeoutParamName";

        private const int _DefaultTimeoutParam = 30000;
        private const string _QueueFibonacciAdressDefaultValue = "rabbitmq://localhost/";
        private const string _QueueFibonacciDefaultValue = "fibonacci_queue";
        private const string _QueueNameDefault = "guest";
        private const string _QueuePasswordDefault = "guest";
        private const string _WebApiServerAddressDefaultValue = "http://localhost:60095/";

        public string QueueFibonacciName
        {
            get
            {
               return  ExtractOrGetDefaultParameter(_QueueFibonacciName, _QueueFibonacciDefaultValue);
            }
        }

        public string ServicePasswordUser
        {
            get
            {
                return ExtractOrGetDefaultParameter(_PasswordName, _QueuePasswordDefault);
            }
        }

        public string ServiceUserName
        {
            get
            {
                return ExtractOrGetDefaultParameter(_UserName, _QueueNameDefault);
            }
        }

        public string QueueFibonacciAdress
        {
            get
            {
                return ExtractOrGetDefaultParameter(_QueueFibonacciName, _QueueFibonacciAdressDefaultValue);
            }
        }

        public string WebApiServerAddress
        {
            get
            {
                return ExtractOrGetDefaultParameter(_WebApiServerAddressName, _WebApiServerAddressDefaultValue);
            }
        }

        public int DefaultWaitingTimeout
        {
            get
            {
                return ExtractOrGetDefaultIntegerParameter(_DefaultTimeoutParamName, _DefaultTimeoutParam);
            }
        }

        private string ExtractOrGetDefaultParameter(string paramName, string defaultParamValue)
        {
            var paramvalue = ConfigurationManager.AppSettings[paramName];
            return paramvalue ?? defaultParamValue;
        }


        private int ExtractOrGetDefaultIntegerParameter(string paramName, int defaultParamValue)
        {
            var paramvalue = ConfigurationManager.AppSettings[paramName];
            int intValue;
            if (int.TryParse(paramvalue, out intValue))
            {
                return intValue;
            }
            else {
                return defaultParamValue;
            }
        }
    }
}
