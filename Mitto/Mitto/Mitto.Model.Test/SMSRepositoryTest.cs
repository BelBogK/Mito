using Mitto.Tests.General;
using Motto.IDataLayer;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Linq;

namespace Mitto.Tests
{
    [TestFixture]
    public class SMSRepositoryTest:BaseTest
    {
        [Test]
        public async Task SMSRepositoryTest_GetSentedSMS_SentedSMSWithCOrrectPrice()
        {
            ReCreateKernal();
            var repository = _kernal.GetService<ISMSRepository>();
            var result =await repository.GetSentedSMS(new DateTime(2020, 01, 01), new DateTime(2020, 03, 05), 100, 0);

            var poland1 = result.Items.Where(x => x.dateTime == new DateTime(2020, 01, 05) && x.mcc == "260");
            var poland2= result.Items.Where(x => x.dateTime == new DateTime(2020, 03, 05) && x.mcc == "260");

            Assert.AreEqual(poland1.First().price, 0.05m);
            Assert.AreEqual(poland2.First().price, 0.22m); 
        }

        [Test]
        public async Task SMSRepositoryTest_GetStatistic_Statistic()
        {
            ReCreateKernal();
            var repository = _kernal.GetService<ISMSRepository>();
            var from = new DateTime(2020, 01, 01);
            var to = new DateTime(2020, 04, 10);

            var allMessageByPeriod =await repository.GetSentedSMS(from, to, 100, 0);
            var statistic = repository.GetStatistic(from, to, new List<string>());

            Assert.AreEqual(allMessageByPeriod.Items.Sum(x => x.price), statistic.Sum(x => x.TotalPrice),"Incorrect control sum for price");
            Assert.AreEqual(allMessageByPeriod.Count, statistic.Sum(x => x.Count),"Incorrect item records");
        }

    }
}
