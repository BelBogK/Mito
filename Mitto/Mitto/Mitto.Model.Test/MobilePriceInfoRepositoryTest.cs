using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Mitto.DataLayer;
using Mitto.DataLayer.Repositorys;
using Mitto.Tests.General;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Motto.IDataLayer;

namespace Mitto.Tests
{
    [TestFixture]
    public class MobilePriceInfoRepositoryTest:BaseTest
    {
        [Test]
        public async Task MobilePriceInfoRepository_GetForCountry_GetForGermany()
        {
            var repository = _kernal.GetService<IMobileOperatorDetailRepository>();
            var countryRepository = _kernal.GetService<ICountryRepository>();

            var result =await repository.GetForCountry((await countryRepository.GetCountryByName("Germany")).ID);
            Assert.AreEqual(result.Count, 3);

        }
    }
}
