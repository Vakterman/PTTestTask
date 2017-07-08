using FinbonacciAsyncLogic.Interfaces;
using FinbonacciAsyncLogic.Entities;
using System;
using MassTransit;

namespace FinbonacciAsyncLogic.Transport
{
    public class RabbitMqBusSender : ISender<FibonacciOperation>
    {
	    private readonly IBusControl _busControl;

        public RabbitMqBusSender(ISenderTransportFactory transportFactory) {
	        _busControl = transportFactory?.CreateSenderControl() ?? throw new ArgumentNullException("transportFactory");
            _busControl.Start();
        }
        public void Send(FibonacciOperation objectForSend)
        {
            _busControl.Publish(objectForSend);
        }

        ~RabbitMqBusSender() {
            _busControl.Stop();
        }
    }
}
