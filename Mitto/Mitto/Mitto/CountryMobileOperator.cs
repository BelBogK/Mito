using System;
using System.Collections.Generic;
using System.Text;

namespace Mitto.Model
{
    public class CountryMobileOperator
    {
        public int MobileOperatorID { get; set; }
        public int CountryId { get; set; }
        public MobileOperator MobileOperator { get; set; }
        public Country Country { get; set; }
    }
}
