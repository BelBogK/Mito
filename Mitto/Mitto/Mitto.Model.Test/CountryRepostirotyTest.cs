using Microsoft.Extensions.DependencyInjection;
using Mitto.DataLayer;
using Mitto.DataLayer.Repositorys;
using Mitto.Tests.General;
using Motto.IDataLayer;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitto.Tests
{
    [TestFixture]
    public class CountryRepostirotyTest: BaseTest
    { 
        [Test]
        public async Task CountryRepostirotyTest_GetCountries_ListCountry()
        {

            var countryRepository = _kernal.GetService<ICountryRepository>();

            var result =await countryRepository.GetCountryInfo();

            var poland = result.First(x => x.Name == "Poland");
            var austria = result.First(x => x.Name == "Austria");
            var germany = result.First(x => x.Name == "Germany");

            Assert.AreEqual(poland.OperatorPrices.First(x => x.MobileOperatorName == "Vodafon").PricePerSMS, 0.1m);
             
        }
    }
}
