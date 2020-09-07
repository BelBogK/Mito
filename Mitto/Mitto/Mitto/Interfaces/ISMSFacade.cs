using Mitto.Model;
using Mitto.Model.DTO;
using Mitto.Model.Enumns;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Motto.Model.Interfaces
{
    public interface ISMSFacade
    {
        Task<EMessageState> SendSMS(SMSDTO sms,bool isTest=false);
        Task<PhoneInfo> ParsePhoneNumber(string phoneNumber);
    }
}
