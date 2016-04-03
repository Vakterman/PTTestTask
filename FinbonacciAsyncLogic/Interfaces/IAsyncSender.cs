using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinbonacciAsyncLogic.Interfaces
{
    public interface IAsyncSender<TInput>
    {
        void SendAsync(TInput objectForSend);
    }
}
