using System.Windows;
using StructureMap;
using FinbonacciAsyncLogic;
using FinbonacciAsyncLogic.Configuration;
using FinbonacciAsyncLogic.Transport;
using FinbonacciAsyncLogic.Interfaces;
using FinbonacciAsyncLogic.Entities;
using FinbonacciAsyncLogic.Logic;

namespace FibonacciApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IContainer Container { get; internal set; }

        protected void InitializeDependency()
        {
            if (Container == null)
            {
                Container = new Container(x =>
                {
                    x.For<IConfigurationManager>().Use<Configuration>();
                    x.For<IAsyncSender<FibonacciOperation>>().Use<RestSharpAsyncSender>();
                    x.For<IAsyncResultHandler<FibonacciOperation>>().Use<RabbitMqResultHandler>();
                    x.For<IFibonacciCalculator<FibonacciOperation>>().Use<FibonacciCalculator>();
                    x.For<IFibonacciCalculator<FibonacciOperation>>().Use<FibonacciCalculator>();
                    x.For<IFibonacciLogicFacade<int>>().Use<FibonacciClientFacade>();
                });

            }
        }

        void Application_SartUp(object sender, StartupEventArgs e)
        {
            InitializeDependency();
        }
    }
}
