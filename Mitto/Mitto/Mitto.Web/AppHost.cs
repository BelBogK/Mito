using ServiceStack;
using Funq; 
using ServiceStack.Configuration;
using ServiceStack.Host.Handlers;
using ServiceStack.Redis;
using ServiceStack.VirtualPath; 
using System.Text;
using ServiceStack.Api.OpenApi;
using Mitto.Web.Services;
using Mitto.DataLayer;
using Motto.IDataLayer;
using Mitto.DataLayer.Repositorys;
using Microsoft.Extensions.Logging;
using Mitto.DataLayer.Servers;
using Motto.Model.Interfaces;
using Mitto.Helper;
using ServiceStack.Text;

namespace Mitto.Web
{
    internal class AppHost : AppHostBase
    {
           // Initializes your AppHost Instance, with the Service Name and assembly containing the Services
        public AppHost() : base("Backbone.js TODO", typeof(CountriesService).Assembly) {}

        // Configure your AppHost with the necessary configuration and dependencies your App needs
        public override void Configure(Container container)
        {
            JsConfig.AssumeUtc = true;

            //Register Redis Client Manager singleton in ServiceStack's built-in Func IOC
            container.Register<IRedisClientsManager>(c =>
                new RedisManagerPool(AppSettings.Get("REDIS_HOST", defaultValue:"localhost")));

            container.Register<ApplicationContext>(s => new ApplicationContext()).ReusedWithin(ReuseScope.Request);
            container.Register<ICurrencyExchangeRepositroy>(s => new CurrencyExchangeRepositroy(s.Resolve<ApplicationContext>())).ReusedWithin(ReuseScope.Request);
            container.Register<IMobileOperatorDetailRepository>(s => new MobileOperatorDetailRepository(s.Resolve<ApplicationContext>())).ReusedWithin(ReuseScope.Request);
            container.Register<IPriceForSmsRepository>(s => new PriceForSmsRepository(s.Resolve<ApplicationContext>())).ReusedWithin(ReuseScope.Request);

            container.Register<ICountryRepository>(s => new CountryRepository(s.Resolve<ApplicationContext>(),
                s.Resolve<ICurrencyExchangeRepositroy>(),
                s.Resolve<IMobileOperatorDetailRepository>(),
                s.Resolve<IPriceForSmsRepository>()
                )).ReusedWithin(ReuseScope.Request); ;

            var logger = LoggerFactory.Create(builder =>
            {
                builder
.AddFilter("Microsoft", LogLevel.Warning)
.AddFilter("System", LogLevel.Warning)
.AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
.AddConsole()
.AddEventLog();
            }).CreateLogger<Program>(); 

            container.Register<ISmsServer>(s => new SMSServer(logger)).ReusedWithin(ReuseScope.Request); ;

            container.Register<ISMSRepository>(s => new SMSRepository(s.Resolve<ApplicationContext>(), s.Resolve<IMobileOperatorDetailRepository>()
                , s.Resolve<ICountryRepository>()
                , s.Resolve<IPriceForSmsRepository>()
                , s.Resolve<ICurrencyExchangeRepositroy>()
                )).ReusedWithin(ReuseScope.Request); ;

            container.Register<ISMSFacade>(s =>
            new SMSFacade(s.Resolve<ISmsServer>(),
            s.Resolve<ISMSRepository>(),
            s.Resolve<IMobileOperatorDetailRepository>(),
            s.Resolve<ICountryRepository>())).ReusedWithin(ReuseScope.Request); 

            Plugins.Add(new OpenApiFeature());
        }
    }
}