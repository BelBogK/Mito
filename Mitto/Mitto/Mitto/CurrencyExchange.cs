using System;

namespace Mitto.Model
{
    public class CurrencyExchange
    {
        public int Id { get; set; }
        public int CurrencyID { get; set; }
        public decimal Rate { get; set; }
        public DateTime AppliedDate { get; set; }
        public virtual Currency Currency { get; set; }
    }
}