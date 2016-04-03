using System;
using FinbonacciAsyncLogic.Entities;
using System.Threading.Tasks;
using MassTransit;

namespace FinbonacciAsyncLogic.Transport
{
    public class MassTransitConusmer : IConsumer<FibonacciOperation>
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
