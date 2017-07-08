using log4net;
using FinbonacciAsyncLogic.Interfaces;

namespace FinbonacciAsyncLogic.Logic
{
    public class Net4LoggerWrapper : ILogger
    {
        private readonly ILog _logger;
        public Net4LoggerWrapper(ILog logger)
        {
            _logger = logger;
        }
        public void LogErrorMessage(string errorMessage)
        {
            _logger.Error(errorMessage);
        }

        public void LogInfoMessage(string infoMessage)
        {
            _logger.Info(infoMessage);
        }
    }
}
