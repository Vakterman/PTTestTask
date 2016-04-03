using System;
using FinbonacciAsyncLogic.Utils;
using FinbonacciAsyncLogic.Interfaces;
using FinbonacciAsyncLogic.Entities;

namespace FinbonacciAsyncLogic
{
    public class FibonacciCalculator : IFibonacciCalculator<FibonacciOperation>
    {
        public FibonacciCalculator() {      
        }
        public virtual void Calculate(FibonacciOperation operationData)
        {
            if (operationData.Value != 1)
            {
                operationData.Value = (operationData.Value + (operationData.Value - 1));
            }
            else
            {
                operationData.Value = 2;
            }

            operationData.CycleCount -= 1;
        }
    }
}
