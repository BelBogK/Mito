using Microsoft.Extensions.Logging;
using Mitto.Model;
using Mitto.Model.DTO;
using Mitto.Model.Enumns;
using Motto.IDataLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mitto.DataLayer.Servers
{
    public class SMSServer : ISmsServer
    {
        #region private members 

        private ILogger _logger;

        #endregion


        #region constructors

        public SMSServer(ILogger logger)
        {
            _logger = logger;
        }

        #endregion

        #region ISmsServer

        public async Task<EMessageState> SendSMS(SMSDTO sms)
        {
            _logger.LogInformation($"SMS sended to {sms.To} from {sms.From}");
            return await Task.Run(()=> { return EMessageState.Success; });
        }
        #endregion 
    }
}
