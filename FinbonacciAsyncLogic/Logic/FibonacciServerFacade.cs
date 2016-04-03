using System;
using FinbonacciAsyncLogic.Utils;
using FinbonacciAsyncLogic.Interfaces;
using FinbonacciAsyncLogic.Entities;

namespace FinbonacciAsyncLogic.Logic
{
    public class FibonacciServerFacade : IFibonacciLogicFacade<FibonacciOperation>
    {
        private IAsyncSender<FibonacciOperation> _asyncSender;
        private IFibonacciCalculator<FibonacciOperation> _calculator;
        private ILogger _logger;

        public FibonacciServerFacade(IAsyncSender<FibonacciOperation> asyncSender, IFibonacciCalculator<FibonacciOperation> calculator, ILogger logger)
        {
            if (asyncSender == null)
            {
                throw new ArgumentNullException("asyncSender");
            }

            if (calculator == null)
            {
                throw new ArgumentNullException("calculator");
            }

            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            _logger = logger;
            _asyncSender = asyncSender;
            _calculator = calculator;
        }
        public FibonacciOperation Evaluate(FibonacciOperation operationObject)
        {
            _logger.LogInfoMessage(String.Format("Вычисление с параметрами количство циклов {0} и значение {1}",operationObject.CycleCount,operationObject.Value));
            if (operationObject.IsOperationInProgress())
            {
                _calculator.Calculate(operationObject);
            }

            _asyncSender.SendAsync(operationObject);
            _logger.LogInfoMessage(String.Format("Вычисление с параметрами количство циклов {0} и значение {1} завершено.", operationObject.CycleCount, operationObject.Value));
            return operationObject;
        }
    }
}
