using System;
using System.Threading.Tasks;
using FinbonacciAsyncLogic.Entities;
using FinbonacciAsyncLogic.Interfaces;
using MassTransit;
using MassTransit.RabbitMqTransport;
using System.Collections.Generic;

namespace FinbonacciAsyncLogic.Transport
{
    public class RabbitMqResultHandler : IAsyncResultHandler<FibonacciOperation>
    {
        private IConfigurationManager _configuration;
        private IBusControl _busController;
        public RabbitMqResultHandler(IConfigurationManager configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            _configuration = configuration;
           
        }
        public bool AddHandler(Action<FibonacciOperation> asyncResultHandler)
        {
            if (_busController == null)
            {
                var consumer = new MassTransitConusmer();
                consumer.AddHandler(asyncResultHandler);

                var busController = CreateAndStartBussControllerWithSpecifiedConsumer(consumer);

                return true;
            }

            return false;
        }

        public void Dispose()
        {
            StopAllBusControllers();
        }

        ~RabbitMqResultHandler() {
            StopAllBusControllers();
        }
        private void StopAllBusControllers() {
            _busController.Stop();
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
        protected sealed class MassTransitConusmer : IConsumer<FibonacciOperation>
        {
            delegate void HandleAsyncResult(FibonacciOperation fibonacciOpeartion);

            private HandleAsyncResult _asyncResultHandlerAsync;

            public void AddHandler(Action<FibonacciOperation> asyncresult)
            {
                _asyncResultHandlerAsync += (result) => asyncresult(result);
            }
            public async Task Consume(ConsumeContext<FibonacciOperation> context)
            {
                var fibonacciOperation = context.Message;

                await Task.Run(() =>
                {
                    _asyncResultHandlerAsync(fibonacciOperation);
                });
            }

        }
    }
}
