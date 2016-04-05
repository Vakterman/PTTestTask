using System;
using FinbonacciAsyncLogic.Interfaces;
using FinbonacciAsyncLogic.Logic;
using FinbonacciAsyncLogic.Entities;
using FinbonacciAsyncLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FibonacciLogicTests.TestLogic;

namespace FibonacciLogicTests
{
    [TestClass]
    public class TestFibonacciMethod
    {
        private FibonacciOperation _initialTestValue;
        private FibonacciAlghoritm _testLogic;
        private FibonacciCalculator _fibonacciCalculator;

        [TestInitialize]
        public void TestInitialize() {
            _initialTestValue = new FibonacciOperation
            {
                Value = 1,
                CycleCount = 0
            };

            _fibonacciCalculator = new FibonacciCalculator();
            _testLogic = new FibonacciAlghoritm();
        }


        [TestMethod]
        public void CheckResult_For10Cycles()
        {

            CheckResult_ForDifferentCycleCount(10);
        }

        [TestMethod]
        public void CheckResult_For20Cycles()
        {

            CheckResult_ForDifferentCycleCount(20);
        }

        [TestMethod]
        public void CheckResult_For30Cycles()
        {

            CheckResult_ForDifferentCycleCount(30);
        }

        [TestMethod]
        public void CheckResult_For50Cycles()
        {

            CheckResult_ForDifferentCycleCount(50);
        }





        private void CheckResult_ForDifferentCycleCount(long cycleCount) {

            _initialTestValue.CycleCount = cycleCount;

            var expectedResult = _testLogic.Evaluate(_initialTestValue.Value, _initialTestValue.CycleCount);

            while (_initialTestValue.CycleCount > 0)
            {
                _fibonacciCalculator.Calculate(_initialTestValue);
            }


            Assert.IsTrue(_initialTestValue.Value == expectedResult, string.Format("Ожидаемый результат {0} не совпадает с полученным {1}", expectedResult, _initialTestValue.Value));
        }
    }
}
