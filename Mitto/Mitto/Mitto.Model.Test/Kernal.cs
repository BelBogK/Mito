using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mitto.DataLayer;
using Mitto.DataLayer.Repositorys;
using Mitto.DataLayer.Servers;
using Mitto.Helper;
using Moq;
using Motto.IDataLayer;
using Motto.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mitto.Tests
{
    public class Kernal
    {
        #region private members

        readonly ApplicationContext _context = new ApplicationContext();

        #endregion
        #region public fields

        public readonly ServiceProvider KERNAL;

        #endregion

        #region Constructor

        public Kernal()
        {
            KERNAL = InitKernal();

        }

        ~Kernal()
        {
            _context.Dispose();
        }

        #endregion

        #region private 

        private ServiceProvider InitKernal()
        { 
            var services = new ServiceCollection();

            services.AddTransient<ApplicationContext>(s => new ApplicationContext());

            services.AddTransient<ICurrencyExchangeRepositroy>(s => new CurrencyExchangeRepositroy(_context));
            services.AddTransient<IMobileOperatorDetailRepository>(s => new MobileOperatorDetailRepository(_context));
            services.AddTransient<IPriceForSmsRepository>(s => new PriceForSmsRepository(_context));

            services.AddTransient<ICountryRepository>(s =>
            new CountryRepository(_context,
            s.GetRequiredService<ICurrencyExchangeRepositroy>(),
            s.GetRequiredService<IMobileOperatorDetailRepository>(),
            s.GetRequiredService<IPriceForSmsRepository>()
            ));

            var logger = Mock.Of<ILogger<ISmsServer>>();
            services.AddTransient<ISmsServer>(s => new SMSServer(logger));

            services.AddTransient<ISMSRepository>(s => new SMSRepository(_context, s.GetRequiredService<IMobileOperatorDetailRepository>()
                , s.GetRequiredService<ICountryRepository>()
                , s.GetRequiredService<IPriceForSmsRepository>()
                ,s.GetRequiredService<ICurrencyExchangeRepositroy>()
                ));

            services.AddTransient<ISMSFacade>(s =>
            new SMSFacade(s.GetRequiredService<ISmsServer>(),
            s.GetRequiredService<ISMSRepository>(),
            s.GetRequiredService<IMobileOperatorDetailRepository>(),
            s.GetRequiredService<ICountryRepository>()));

            return services.BuildServiceProvider();
        }

        #endregion

    }
}
