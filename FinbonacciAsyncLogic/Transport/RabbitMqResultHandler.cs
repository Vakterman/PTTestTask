using System;
using FinbonacciAsyncLogic.Entities;
using FinbonacciAsyncLogic.Interfaces;
using MassTransit;

namespace FinbonacciAsyncLogic.Transport
{
    public class RabbitMqResultHandler : IAsyncResultHandler<FibonacciOperation>
    {
        private IConfigurationManager _configuration;
        private IBusControl _busController;
        public RabbitMqResultHandler(IConfigurationManager configuration)
        {
			_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
           
        }
        public bool AddHandler(Action<FibonacciOperation> asyncResultHandler)
        {
	        if (_busController != null) return false;
	        var consumer = new MassTransitConusmer();
	        consumer.AddHandler(asyncResultHandler);

	        _busController = CreateAndStartBussControllerWithSpecifiedConsumer(consumer);

	        return true;
        }

        public void Dispose()
        {
	        _busController?.Stop();
        }

        ~RabbitMqResultHandler()
        {
	        _busController?.Stop();
        }
     
        private IBusControl CreateAndStartBussControllerWithSpecifiedConsumer(MassTransitConusmer consumerInstance)
        {
            var busControl = CreateBusController(consumerInstance);

            busControl.Start();

            return busControl;
        }

        private IBusControl CreateBusController(IConsumer<FibonacciOperation> consumerInstance)
        {
            return  Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(_configuration.QueueFibonacciAdress), h =>
                {
                    h.Username(_configuration.ServiceUserName);
                    h.Password(_configuration.ServicePasswordUser);
                });

                cfg.ReceiveEndpoint(host, _configuration.QueueFibonacciName, e =>
                {
                    e.Instance(consumerInstance);
                });
            });
        }
    }
}
