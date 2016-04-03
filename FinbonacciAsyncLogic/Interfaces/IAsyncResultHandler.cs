using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinbonacciAsyncLogic.Entities;

namespace FinbonacciAsyncLogic.Interfaces
{
    public interface IAsyncResultHandler<TAsyncResult>
    {
        bool AddHandler(Action<FibonacciOperation> asyncResultHandler);
    }
}
