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
        private ILogger _logger;
        private IAsyncResultHandler<FibonacciOperation> _fibonacciResultReceiver;
        private IAsyncSender<FibonacciOperation> _asyncSender;
        private IFibonacciCalculator<FibonacciOperation> _calculator;

        public FibonacciClientFacade(IConfigurationManager configurationManager, IAsyncSender<FibonacciOperation> senderAsyncOperations, IAsyncResultHandler<FibonacciOperation> fibonacciResultReceiver,IFibonacciCalculator<FibonacciOperation> calculator, ILogger logger){

            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

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
            _logger = logger;

            
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
            _logger.LogInfoMessage(String.Format("Количество циклов:{0},текущий результат {1}", operationResult.CycleCount, operationResult.Value));

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

                _logger.LogInfoMessage(String.Format("Завершение вычислений текущий результат {0}", operationResult.Value));

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
