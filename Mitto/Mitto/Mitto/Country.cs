using System;
using System.Collections.Generic;
using System.Text;

namespace Mitto.Model
{
    public class Country
    {
        public int ID { get; set; }
        public string Name { get; set; }         
        public int CountryCode { get; set; }
        public int CurrencyID { get; set; }
        /// <summary>
        /// General information about Currency. You can find rates in Currency.CurrencyExchanges. Pay attantion all rates has AppliedDate
        /// </summary>
        public Currency Currency { get; set; }

        /// <summary>
        /// We can have one Parthner who work in few countries and has difference prices for each county. That why in price for country store in MobilePriceInfo
        /// </summary>
        public virtual ICollection<CountryMobileOperator> MobileOperators { get; set; }

        public Country()
        {
            MobileOperators = new HashSet<CountryMobileOperator>();
        }
    }
}
