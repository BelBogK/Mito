using Mitto.Model.Enumns;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mitto.Model
{
    public class Currency
    {
        public int ID { get; set; } 
        public string CurrencyName { get; set; }
        public string CurrencySymbol { get; set; }
        public bool IsPrime { get; set; }
        public virtual ICollection<CurrencyExchange> CurrencyExchanges { get; set; }

    }
}
