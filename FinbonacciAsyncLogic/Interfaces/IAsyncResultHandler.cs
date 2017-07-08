using System;
using FinbonacciAsyncLogic.Entities;

namespace FinbonacciAsyncLogic.Interfaces
{
    public interface IAsyncResultHandler<TAsyncResult>
    {
        bool AddHandler(Action<FibonacciOperation> asyncResultHandler);
    }
}
