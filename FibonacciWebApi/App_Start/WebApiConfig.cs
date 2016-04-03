using System.Web.Http;
using StructureMap;
using FinbonacciAsyncLogic;
using FinbonacciAsyncLogic.Entities;
using FinbonacciAsyncLogic.Interfaces;
using FinbonacciAsyncLogic.Configuration;
using FinbonacciAsyncLogic.Transport;
using FinbonacciAsyncLogic.Utils;
using FinbonacciAsyncLogic.Logic;
using log4net;
using System.Reflection;

namespace FibonacciWebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{val}/{cyclecount}",
                defaults: new { val = RouteParameter.Optional, cyclecount = RouteParameter.Optional }
            );

            config.DependencyResolver = new DependencyResolver(CreateAndConfigureContainer());

            log4net.Config.XmlConfigurator.Configure();

        }

        private static IContainer CreateAndConfigureContainer()
        {
            var container = new Container(x => {

                x.For<ILog>().Use(LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType));
                x.For<ILogger>().Use<Net4LoggerWrapper>();
                x.For<IConfigurationManager>().Use<Configuration>();
                x.For<ISenderTransportFactory>().Use<RabbitMqTransportFactory>();
                x.For<IAsyncSender<FibonacciOperation>>().Use<RabbitMqBusSender>();
                x.For<IFibonacciCalculator<FibonacciOperation>>().Use<FibonacciCalculator>();
                x.For<IFibonacciLogicFacade<FibonacciOperation>>().Use<FibonacciServerFacade>();
            });

            return container;
        }
    }
}
