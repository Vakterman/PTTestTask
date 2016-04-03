using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinbonacciAsyncLogic.Utils;
using FinbonacciAsyncLogic.Interfaces;
using FinbonacciAsyncLogic.Entities;

namespace FinbonacciAsyncLogic.Logic
{
    public class FibonacciServerFacade : IFibonacciLogicFacade<FibonacciOperation>
    {
        private IAsyncSender<FibonacciOperation> _asyncSender;
        private IFibonacciCalculator<FibonacciOperation> _calculator;

        public FibonacciServerFacade(IAsyncSender<FibonacciOperation> asyncSender, IFibonacciCalculator<FibonacciOperation> calculator)
        {
            if (asyncSender == null)
            {
                throw new ArgumentNullException("_asyncSender");
            }

            if (calculator == null)
            {
                throw new ArgumentNullException("_calculator");
            }

            _asyncSender = asyncSender;
            _calculator = calculator;
        }
        public FibonacciOperation Evaluate(FibonacciOperation operationObject)
        {
            if (operationObject.IsOperationInProgress())
            {
                _calculator.Calculate(operationObject);
            }

            _asyncSender.SendAsync(operationObject);

            return operationObject;
        }
    }
}
