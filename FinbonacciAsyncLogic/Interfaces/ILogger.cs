using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinbonacciAsyncLogic.Interfaces
{
    //Простой интерфей для потребностей приложения.
    public interface ILogger
    {
        void LogInfoMessage(string infoMessage);

        void LogErrorMessage(string errorMessage);
    }
}
