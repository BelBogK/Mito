using Microsoft.Extensions.DependencyInjection;
using Mitto.DataLayer;
using Mitto.DataLayer.Repositorys;
using Mitto.DataLayer.Servers;
using Mitto.Helper;
using Mitto.Model;
using Mitto.Model.DTO;
using Mitto.Tests.General;
using Motto.IDataLayer;
using Motto.Model.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mitto.Tests
{
    [TestFixture]
    public class SMSFacadeTest:BaseTest
    {

        #region constructor

        public SMSFacadeTest()
        {
        }

        #endregion

        [Test]
        public async Task SMSFacade_ParsePhoneNumber_ParsedPhone()
        {
            var phoneNumber = "49262123456";
            var etalon = new PhoneInfo { CountryCode = "49", MobileCode = "262", PhoneNumber = "123456" };

            var facade = _kernal.GetService<ISMSFacade>();

            var result = await facade.ParsePhoneNumber(phoneNumber);
            Assert.AreEqual(etalon.CountryCode, result.CountryCode,"Country code is incorrect");
            Assert.AreEqual(etalon.MobileCode, result.MobileCode, "Mobile code is incorrect");
            Assert.AreEqual(etalon.PhoneNumber, result.PhoneNumber,"Phone number is incorrect");
        }

        [Test]
        public void SMSFacade_ParsePhoneNumber_ExceptionNotFoundCountry()
        {
            var facade = _kernal.GetService<ISMSFacade>();

            Assert.ThrowsAsync(Is.TypeOf<ArgumentOutOfRangeException>().And.Message.Contains("Invalid country code for number"),
                async()=>{ await facade.ParsePhoneNumber("123345234234"); });

        }

        [Test]
        public void SMSFacade_ParsePhoneNumber_ExceptionNotFoundMobileCOde()
        {
            var facade = _kernal.GetService<ISMSFacade>();

            Assert.ThrowsAsync(Is.TypeOf<ArgumentOutOfRangeException>().And.Message.Contains("Invalid mobile code for number"),
                  async()=>await facade.ParsePhoneNumber("492123345234234") );

        }

        [Test]
        public async Task SMSFacade_SendSMS_RecordInDb()
        {
            var smsRepository = _kernal.GetService<ISMSRepository>();
            var facade = _kernal.GetService<ISMSFacade>();

            var smsDTO = new SMSDTO()
            {
                From = "49262123456",
                To = "49292987654321",
                Text = "Some text from test"
            };

            var messagesBeforeSend =await smsRepository.GetCountOfSMS();
            await facade.SendSMS(smsDTO, false);
            var messagesAfterSendSMS = await smsRepository.GetCountOfSMS();

            Assert.AreEqual(messagesBeforeSend+1, messagesAfterSendSMS , "Didn't safe message");

        }

        [Test]
        public async Task SMSFacade_SendSMSPoland_RecordInDb()
        {
            var smsRepository = _kernal.GetService<ISMSRepository>();
            var facade = _kernal.GetService<ISMSFacade>();

            var smsDTO = new SMSDTO()
            {
                From = "48250123456",
                To = "48250987654321",
                Text = "Some text from test"
            };

            var messagesBeforeSend = await smsRepository.GetCountOfSMS();
            await facade.SendSMS(smsDTO, false);
            var messagesAfterSendSMS = await smsRepository.GetCountOfSMS();

            Assert.AreEqual(messagesBeforeSend + 1, messagesAfterSendSMS, "Didn't safe message");
        }
    }
}
