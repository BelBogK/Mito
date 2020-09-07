using Mitto.Model;
using Mitto.Model.DTO;
using Mitto.Model.Enumns;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Motto.IDataLayer
{
    public interface ISMSRepository:IDisposable
    {
        Task<bool> Save(SMS sms);
        Task<int> GetCountOfSMS();
        Task<IEnumerable<SMS>> GetAllMessageSendedBy(PhoneInfo pi);
        Task<SMS> GetLastSMS();
        Task<SentedSMSConteinerDTO> GetSentedSMS(DateTime from, DateTime to, int take, int skip);
        IEnumerable<SMSStatisticTDO> GetStatistic(DateTime from, DateTime to, IEnumerable<String> mccList);
    }
}
