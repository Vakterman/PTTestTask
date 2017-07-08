using FinbonacciAsyncLogic.Entities;
using FinbonacciAsyncLogic.Interfaces;

namespace FinbonacciAsyncLogic.Logic
{
    public class FibonacciCalculator : IFibonacciCalculator<FibonacciOperation>
    {
	    public virtual void Calculate(FibonacciOperation operationData)
        {
            if (operationData.Value != 1)
            {
                operationData.Value = operationData.Value + (operationData.Value - 1);
            }
            else
            {
                operationData.Value = 2;
            }

            operationData.CycleCount -= 1;
        }
    }
}
