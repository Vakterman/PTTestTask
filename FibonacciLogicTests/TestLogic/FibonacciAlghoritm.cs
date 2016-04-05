using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FibonacciLogicTests.TestLogic
{
    public class FibonacciAlghoritm
    {
        public long Evaluate(long startValue, long cyclesCount) {
            long result = startValue;
            for (int i = 0; i < cyclesCount; i++)
            {
                result = FibonacciEvaluate(result);
            }

            return result;
        }

        private long FibonacciEvaluate(long sourceValue) {
            long result;
            if (sourceValue == 1)
            {
                return 2;
            } else
            {
                result = sourceValue + (sourceValue - 1);
            }
            
            return result;
        }
    }
}
