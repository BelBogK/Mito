using System;
using System.Collections.Generic;
using System.Text;

namespace Mitto.Model.DTO
{
    public class MobileOperatorDTO
    {
        public string MobileOperatorName { get; set; }
        public decimal PricePerSMS { get; set; }
        /// <summary>
        /// the mobile country code of the country
        /// </summary>
        public int MCC { get; set; }
    }
}
