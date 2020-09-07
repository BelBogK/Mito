using System;
using System.Collections.Generic;
using System.Text;

namespace Mitto.Model.DTO
{
    public class CountryDTO
    { 
        /// <summary>
        /// The country code of the country
        /// </summary>
        public string CC { get; set; }
        public string Name { get; set; }
        public IEnumerable<MobileOperatorDTO> OperatorPrices { get; set; }
    }
}
