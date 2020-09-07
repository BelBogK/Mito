using Mitto.Model.DTO;
using Mitto.Model.Enumns;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mitto.Model
{
    public class SMS
    {
        #region public property
        public int ID { get; set; }
        public string Text { get; set; }
        public string ToCountryCode { get; set; }
        public string ToMobileCode { get; set; }
        public string ToNumber { get; set; }
        public string FromCountryCode { get; set; }
        public string FromMobileCode { get; set; }
        public string FromNumber { get; set; }
        public DateTime Sended { get; set; }
        public EMessageState State { get; set; }

        public string ToPhoneNumber { get { return ToCountryCode + ToMobileCode + ToNumber; } }
        public string FromPhoneNumber { get { return FromCountryCode + FromMobileCode + FromNumber; } }
        #endregion

        #region constructors

        public SMS()
        {

        }

        public SMS(SMSDTO sms, PhoneInfo from, PhoneInfo to)
        {
            Text = sms.Text;
            Sended = sms.Sended;
            State = sms.State;

            ToCountryCode = to.CountryCode;
            ToMobileCode = to.MobileCode;
            ToNumber = to.PhoneNumber;

            FromCountryCode = from.CountryCode;
            FromMobileCode = from.MobileCode;
            FromNumber = from.PhoneNumber;
        }
        #endregion

    }
}
