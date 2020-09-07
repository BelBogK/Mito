using System;
using System.Collections.Generic;

namespace Mitto.Model
{
    /// <summary>
    /// Mobile operator agency in country
    /// </summary>
    public class MobileOperatorDetail
    {
        public int ID { get; set; }
        public int MobileOperatorID { get; set; }
        public int MobileCountryCode { get; set; }
        public int CountryID { get; set; }
        public List<PriceForSMS> PricesForSMSs { get; set; }
        public MobileOperator MobileOperator { get; set; }
        public Country Country { get; set; }
    }
}