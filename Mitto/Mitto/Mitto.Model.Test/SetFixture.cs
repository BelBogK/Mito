using Microsoft.Extensions.DependencyInjection;
using Mitto.Model.DTO;
using Mitto.Tests.General;
using Motto.IDataLayer;
using Motto.Model.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitto.Tests
{
    [SetUpFixture]
    public class SetFixture:BaseTest
    {
        [OneTimeSetUp]
        public async Task SetUpData()
        {
            await InitSMSesIfNeed();
        }

        private async Task InitSMSesIfNeed()
        {
            var sender = _kernal.GetService<ISMSFacade>();
            var smsRepository = _kernal.GetService<ISMSRepository>();
            SMSDTO sms;

            var smses=await smsRepository.GetSentedSMS(DateTime.MinValue, DateTime.Now, 100, 0);
            var poland = smses.Count==0 ? null : smses.Items.FirstOrDefault(x => x.dateTime == new DateTime(2020, 01, 05) && x.mcc == "260");
            if(poland==null)
            {
                sms = new SMSDTO
                {
                    From = "48260123456789",
                    Text = "2020, 01, 05",
                    To = "4826056345f",
                    Sended=new DateTime(2020, 01, 05)
                };
                await sender.SendSMS(sms, true);
            }

            poland = smses.Count == 0 ? null : smses.Items.FirstOrDefault(x => x.dateTime == new DateTime(2020, 03, 05) && x.mcc == "260");
            if (poland == null)
            {
                sms = new SMSDTO
                {
                    From = "48260123456789",
                    Text = "2020, 03, 05",
                    To = "4826056345f",
                    Sended = new DateTime(2020, 03, 05)
                };
                await sender.SendSMS(sms, true);
            }

        }
    }
}
