using FinbonacciAsyncLogic.Interfaces;
using FinbonacciAsyncLogic.Entities;
using System;
using MassTransit;
using MassTransit.RabbitMqTransport;

namespace FinbonacciAsyncLogic.Transport
{
    public class RabbitMqBusSender : IAsyncSender<FibonacciOperation>
    {
        private ITransportFactory _transportFactory;
        private IBusControl _busControl;

        public RabbitMqBusSender(ITransportFactory transportFactory) {
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
