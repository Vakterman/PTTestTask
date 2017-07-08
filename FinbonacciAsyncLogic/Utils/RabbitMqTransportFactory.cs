using System;
using FinbonacciAsyncLogic.Interfaces;
using MassTransit;
using MassTransit.Transports;

namespace FinbonacciAsyncLogic.Utils
{
    public class RabbitMqTransportFactory : ISenderTransportFactory
    {
        private readonly IConfigurationManager _configurationManager;

        public RabbitMqTransportFactory(IConfigurationManager configurationManager)
        {
			_configurationManager = configurationManager ?? throw new ArgumentNullException(nameof(configurationManager));
        }

        public IBusControl CreateSenderControl()
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
	            cfg.Host(new Uri(_configurationManager.QueueFibonacciAdress), h =>
	            {
		            h.Username(_configurationManager.ServiceUserName);
		            h.Password(_configurationManager.ServicePasswordUser);
	            });
            });

            return busControl;
        }
    }
}
