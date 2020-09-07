using System;
using System.Collections.Generic;
using System.Text;

namespace Mitto.Model
{
    public class PriceForSMS
    {
        public int ID { get; set; }
        public int MobileOperatorDetailID { get; set; }
        /// <summary>
        /// When was changed last time Price for SMS (Min date in db for Mobile Operator mean start work with that operator)
        /// </summary>
        public DateTime AppliedDate { get; set; }

        /// <summary>
        /// In local currency
        /// </summary>
        public decimal Price { get; set; }
        
        public virtual MobileOperatorDetail OperatorDetail { get; set; }
    }
}
