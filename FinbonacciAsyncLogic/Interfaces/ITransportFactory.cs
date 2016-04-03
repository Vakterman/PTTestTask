using MassTransit.Transports;
using MassTransit;

namespace FinbonacciAsyncLogic.Interfaces
{
    public interface ITransportFactory
    {
        IBusControl CreateSenderControl();
    }
}
