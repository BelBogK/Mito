using Mitto.Model.Enumns;
using Mitto.Web.ServicesDTOs;
using Motto.IDataLayer;
using ServiceStack;
using System;
using System.Threading.Tasks;

namespace Mitto.Web.Services
{
    public class SendSMSService:Service
    {
        #region private members

        private ISmsServer _smsServer;
        private ISMSRepository _smsRepository;

        #endregion

        #region constructors

        public SendSMSService(ISmsServer smsServer, ISMSRepository sMSRepository)
        {
            _smsRepository = sMSRepository;
            _smsServer = smsServer;
        }

        #endregion

        public async Task<Object>Get(SMSDTOService sms)
        {
            return await _smsServer.SendSMS(sms);
        }

        public async Task<Object> Get(GetSentSMSDTOService filter)
        {
            var result= await _smsRepository.GetSentedSMS(filter.dateTimeFrom, filter.dateTimeTo, filter.take, filter.skip);
            _smsRepository.Dispose();
            return result;
        }
    }
}
