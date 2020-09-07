using Mitto.Web.ServicesDTOs;
using Motto.IDataLayer;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mitto.Web.Services
{
    public class StatisticsService:Service
    {
        #region private members

        private ISMSRepository _smsRepository;

        #endregion

        #region constructor

        public StatisticsService(ISMSRepository sMSRepository)
        {
            _smsRepository = sMSRepository;
        }

        #endregion

        public Object Get(StatisticDTOService sms)
        {
            return  _smsRepository.GetStatistic(sms.DateFrom, sms.DateTo, sms.MCCList?.Split(","));
        }
    }
}
