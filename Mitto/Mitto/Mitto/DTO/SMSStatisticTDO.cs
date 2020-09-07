using System;
using System.Collections.Generic;
using System.Text;

namespace Mitto.Model.DTO
{
    public class SMSStatisticTDO
    {
        public DateTime Date { get; set; }
        public string Mcc { get; set; }
        public decimal PricePerSms { get; set; }
        public int Count { get; set; }
        public decimal TotalPrice { get { return PricePerSms * Count; } }
    }
}
