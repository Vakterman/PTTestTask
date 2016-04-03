using System.Web.Http;
using StructureMap;
using FinbonacciAsyncLogic;
using FinbonacciAsyncLogic.Entities;
using FinbonacciAsyncLogic.Interfaces;
using FinbonacciAsyncLogic.Configuration;
using FinbonacciAsyncLogic.Transport;
using FinbonacciAsyncLogic.Utils;
using FinbonacciAsyncLogic.Logic;

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
          
        }

        private static IContainer CreateAndConfigureContainer()
        {
            var container = new Container(x => {
                x.For<IConfigurationManager>().Use<Configuration>();
                x.For<ITransportFactory>().Use<RabbitMqTransportFactory>();
                x.For<IAsyncSender<FibonacciOperation>>().Use<RabbitMqBusSender>();
                x.For<IFibonacciCalculator<FibonacciOperation>>().Use<FibonacciCalculator>();
                x.For<IFibonacciLogicFacade<FibonacciOperation>>().Use<FibonacciServerFacade>();
            });

            return container;
        }
    }
}
