using System;
using FinbonacciAsyncLogic.Interfaces;
using MassTransit;
using MassTransit.Transports;

namespace FinbonacciAsyncLogic.Utils
{
    public class RabbitMqTransportFactory : ISenderTransportFactory
    {
        private IConfigurationManager _configurationManager;

        public RabbitMqTransportFactory(IConfigurationManager configurationManager)
        {
            if (configurationManager == null)
            {
                throw new ArgumentNullException("configurationManager");
            }

            _configurationManager = configurationManager;
        }

        public IBusControl CreateSenderControl()
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(_configurationManager.QueueFibonacciAdress), h =>
                {
                    h.Username(_configurationManager.ServiceUserName);
                    h.Password(_configurationManager.ServicePasswordUser);
                });
            });

            return busControl;
        }
    }
}
