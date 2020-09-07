using System;
using System.Collections.Generic;
using System.Text;

namespace Mitto.Model.DTO
{
    public class SentedSms
    {
        /// <summary>
        /// Mobile country code of the country where the receiver of the SMS belong
        /// </summary>
        public string MCC { get; set; }
        /// <summary>
        /// Sender
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// Price of the SMS in EUR
        /// </summary>
        public decimal Price { get; set; }

    }
}
