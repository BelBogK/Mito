using System;
using System.Collections.Generic;
using System.Text;

namespace Mitto.Model.DTO
{
    public class SentedSMSDTO
    {
        public DateTime dateTime { get; set; }

        /// <summary>
        /// the mobile country code of the country where the receiver of the SMS belongs to
        /// </summary>
        public string mcc { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public decimal price { get; set; }
    }
}
