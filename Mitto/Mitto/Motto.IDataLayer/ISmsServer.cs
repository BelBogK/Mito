using Mitto.Model;
using Mitto.Model.DTO;
using Mitto.Model.Enumns;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Motto.IDataLayer
{
    public interface ISmsServer
    {
        public Task<EMessageState> SendSMS(SMSDTO sms);
    }
}
