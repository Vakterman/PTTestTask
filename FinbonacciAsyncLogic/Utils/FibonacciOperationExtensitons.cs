using FinbonacciAsyncLogic.Entities;

namespace FinbonacciAsyncLogic.Utils
{
    public static class FibonacciOperationExtensitons
    {
        public static bool IsOperationInProgress(this FibonacciOperation operation)
        {
            return operation.CycleCount > 0;
        }

        public static bool IsOperationCompleted(this FibonacciOperation operation)
        {
            return operation.CycleCount < 1;
        }
    }
}
