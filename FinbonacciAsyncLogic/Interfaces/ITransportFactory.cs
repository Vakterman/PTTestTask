using MassTransit.Transports;
using MassTransit;

namespace FinbonacciAsyncLogic.Interfaces
{
    public interface ISenderTransportFactory
    {
        IBusControl CreateSenderControl();
    }
}
