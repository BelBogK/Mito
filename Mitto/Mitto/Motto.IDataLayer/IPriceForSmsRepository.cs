using Mitto.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Motto.IDataLayer
{
    public interface IPriceForSmsRepository
    {

        /// <summary>
        /// Return last price for SMS 
        /// </summary>
        /// <param name="mobileOperatorDetailID"></param>
        /// <returns></returns>
        Task<PriceForSMS> GetPriceForSMS(int mobileOperatorDetailID);
        Task<IEnumerable<PriceForSMS>> GetPriceForSMS(int mobileOperatorDetailID, DateTime startTime, DateTime endTime);
    }
}
