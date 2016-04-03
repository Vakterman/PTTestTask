using System;
using System.Threading;
using FinbonacciAsyncLogic.Interfaces;
using FinbonacciAsyncLogic.Entities;
using FinbonacciAsyncLogic.Utils;


namespace FinbonacciAsyncLogic.Logic
{
    public class FibonacciClientFacade : IFibonacciLogicFacade<int>
    {
        private const int  StartFibonacciValue = 1;

        private IConfigurationManager _configurationManager;
        private AutoResetEvent _operationCompleteEvent;
        private FibonacciOperation _result;

        IAsyncResultHandler<FibonacciOperation> _fibonacciResultReceiver;
        IAsyncSender<FibonacciOperation> _asyncSender;
        IFibonacciCalculator<FibonacciOperation> _calculator;

        public FibonacciClientFacade(IConfigurationManager configurationManager, IAsyncSender<FibonacciOperation> senderAsyncOperations, IAsyncResultHandler<FibonacciOperation> fibonacciResultReceiver,IFibonacciCalculator<FibonacciOperation> calculator){
            if (configurationManager == null)
            {
                throw new ArgumentNullException("configurationManager");
            }


            if (senderAsyncOperations == null)
            {
                throw new ArgumentNullException("senderAsyncOperations");
            }

            if (fibonacciResultReceiver == null)
            {
                throw new ArgumentNullException("fibonacciResultReceiver");
            }

            if (calculator == null)
            {
                throw new ArgumentNullException("calculator");
            }

            _operationCompleteEvent = new AutoResetEvent(false);
            _configurationManager = configurationManager;
            _asyncSender = senderAsyncOperations;
            _fibonacciResultReceiver = fibonacciResultReceiver;
            _calculator = calculator;

            
        }
        public int Evaluate(int tryCount)
        {
            _result = null;

            _fibonacciResultReceiver.AddHandler(OperationAsyncResultHandler);

            CalculateAndSendFirstCycle(tryCount);

            if (_operationCompleteEvent.WaitOne(_configurationManager.DefaultWaitingTimeout))
            {
                if (_result != null)
                {
                    return _result.Value;
                }
                else {
                    return 0;
                }
            }

            return -1;
           
        }

        private void OperationAsyncResultHandler(FibonacciOperation operationResult)
        {
            if (operationResult.IsOperationInProgress())
            {
                _calculator.Calculate(operationResult);
            }

            if (operationResult.IsOperationInProgress())
            {
                _asyncSender.SendAsync(operationResult);

            }
            else if (operationResult.IsOperationCompleted())
            {
                _result = new FibonacciOperation()
                {
                    Value = operationResult.Value,
                    CycleCount = operationResult.CycleCount
                };

                _operationCompleteEvent.Set();
            }
        }

        private void CalculateAndSendFirstCycle(int tryCount) {

            var operationObject = new FibonacciOperation
            {
                Value = StartFibonacciValue,
                CycleCount = tryCount
            };

            _calculator.Calculate(operationObject);

            _asyncSender.SendAsync(operationObject);
        }
    }
}
