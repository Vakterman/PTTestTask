using FinbonacciAsyncLogic.Interfaces;
using FinbonacciAsyncLogic.Entities;
using System;
using MassTransit;

namespace FinbonacciAsyncLogic.Transport
{
    public class RabbitMqBusSender : IAsyncSender<FibonacciOperation>
    {
        private ISenderTransportFactory _transportFactory;
        private IBusControl _busControl;

        public RabbitMqBusSender(ISenderTransportFactory transportFactory) {
            if (transportFactory == null)
            {
                throw new ArgumentNullException("transportFactory");
            }

            _transportFactory = transportFactory;
            _busControl = transportFactory.CreateSenderControl();
            _busControl.Start();
        }
        public void SendAsync(FibonacciOperation objectForSend)
        {
            _busControl.Publish(objectForSend);
        }

        ~RabbitMqBusSender() {
            _busControl.Stop();
        }
    }
}
