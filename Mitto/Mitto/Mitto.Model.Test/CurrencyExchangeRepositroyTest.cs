using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Mitto.Tests.General;
using Motto.IDataLayer;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Mitto.Tests
{
    [TestFixture]
    public class CurrencyExchangeRepositroyTest:BaseTest
    {
        [Test]
        public async Task CurrencyExchangeRepositroyTest_GetLastCurrencyExForCountry_LastCurrencyForPoland()
        {
            var exchangeRepostitory = _kernal.GetService<ICurrencyExchangeRepositroy>();
            var countryRepository = _kernal.GetService<ICountryRepository>();
            var country =await countryRepository.GetCountryByName("Poland");

            var result = await exchangeRepostitory.GetLastCurrencyExForCountry(await countryRepository.GetCountryByName("Poland"));
            Assert.AreEqual(result.Rate, 0.22m, "Load incorrect CurrencyEx");
        }
    }
}
