using Mitto.Model;
using Mitto.Model.DTO;
using Mitto.Model.Enumns;
using Motto.IDataLayer;
using Motto.Model.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitto.Helper
{
    public class SMSFacade : ISMSFacade
    {
        #region CASH
        /// <summary>
        /// key- country code, values mobile code
        /// </summary>
        private static readonly ConcurrentDictionary<string, List<string>> PHONE_CODES=new ConcurrentDictionary<string, List<string>>();

        #endregion

        #region private members

        private ISmsServer _smsServer;
        private ISMSRepository _sMSRepository;
        private IMobileOperatorDetailRepository _mobileOperatorDetail;
        private ICountryRepository _countryRepository;

        #endregion

        #region constructor

        public SMSFacade(ISmsServer smsServer, ISMSRepository sMSRepository, IMobileOperatorDetailRepository mobileOperatorDetailRepository, ICountryRepository countryRepository)
        {
            _smsServer = smsServer;
            _sMSRepository = sMSRepository;
            _mobileOperatorDetail = mobileOperatorDetailRepository;
            _countryRepository = countryRepository;
        }

        #endregion

        #region ISMSFacade
        //TODO: Depence how many connection we can do to smeServer
        public async Task<EMessageState> SendSMS(SMSDTO sendedSMS, bool isTest=false)
        {
            //TODO: If server and DB in same machine in another way move that to ISMSRepository
            if (!isTest)
                sendedSMS.Sended = DateTime.Now;

            var result =await _smsServer.SendSMS(sendedSMS);
            sendedSMS.State = result;


            var from =await ParsePhoneNumber(sendedSMS.From);
            var to = await ParsePhoneNumber(sendedSMS.To);

            var sms = new SMS(sendedSMS, from, to);

            await _sMSRepository.Save(sms);

            return result;
        }

        //TODO: Rewrite that method to more flexiable. Currently he can find only when you CountryCode length is 2 and mobile code length is 3
        public async Task<PhoneInfo> ParsePhoneNumber(string phoneNumber)
        {
            PhoneInfo result;

            var lengthCountryCode = 2;
            var lengthMobileCode = 3;
            var countryCode = String.Concat(phoneNumber.Take(lengthCountryCode));
            var mobileCode = String.Concat(phoneNumber.Skip(lengthCountryCode).Take(lengthMobileCode));
            var countryId = await _countryRepository.GetCountryIDByCountryCode(Convert.ToInt32(countryCode));

            if (countryId==-1)
                throw new ArgumentOutOfRangeException($"Invalid country code for number {phoneNumber}");

            var mobileCodeIsExist = PHONE_CODES.GetOrAdd(countryCode, (x) => _mobileOperatorDetail.GetForCountry(countryId).Result.Select(x => x.MobileCountryCode.ToString()).ToList());
            if (!mobileCodeIsExist.Contains(mobileCode))
                throw new ArgumentOutOfRangeException($"Invalid mobile code for number {phoneNumber}");

            result = new PhoneInfo { CountryCode = countryCode, MobileCode = mobileCode, PhoneNumber = String.Concat(phoneNumber.Skip(lengthCountryCode + lengthCountryCode + 1)) };

            return result;
        }
        #endregion

    }
}
