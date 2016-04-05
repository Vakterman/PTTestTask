using System.Windows;
using StructureMap;
using FinbonacciAsyncLogic;
using FinbonacciAsyncLogic.Configuration;
using FinbonacciAsyncLogic.Transport;
using FinbonacciAsyncLogic.Interfaces;
using FinbonacciAsyncLogic.Entities;
using FinbonacciAsyncLogic.Logic;
using log4net;
using System.Reflection;

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
                    x.For<ILog>().Use(LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType));
                    x.For<ILogger>().Use<Net4LoggerWrapper>();
                    x.For<IConfigurationManager>().Use<Configuration>();
                    x.For<IAsyncSender<FibonacciOperation>>().Use<RestSharpAsyncSender>();
                    x.For<IAsyncResultHandler<FibonacciOperation>>().Use<RabbitMqResultHandler>();
                    x.For<IFibonacciCalculator<FibonacciOperation>>().Use<FibonacciCalculator>();
                    x.For<IFibonacciCalculator<FibonacciOperation>>().Use<FibonacciCalculator>();
                    x.For<IFibonacciLogicFacade<long>>().Use<FibonacciClientFacade>();
                });

            }
        }

        void Application_SartUp(object sender, StartupEventArgs e)
        {
            InitializeDependency();

            log4net.Config.XmlConfigurator.Configure();
        }
    }
}
