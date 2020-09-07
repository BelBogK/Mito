using System.Collections;
using System.Collections.Generic;

namespace Mitto.Model
{
    public class MobileOperator
    {
        #region properties
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<MobileOperatorDetail> MobileOperatorDetails { get; set; }
        public virtual ICollection<CountryMobileOperator> Countries { get; set; }
        #endregion

        #region constructors
        public MobileOperator()
        {
            Countries = new HashSet<CountryMobileOperator>();
            MobileOperatorDetails = new HashSet<MobileOperatorDetail>();
        }
        #endregion
    }
}