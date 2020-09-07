using Mitto.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Motto.IDataLayer
{
    /// <summary>
    /// We can only add currency
    /// </summary>
    public interface ICurrencyRepository
    {
        Task<bool> Add(Currency currency);
        /// <summary>
        /// If we add something wrong you could edit
        /// </summary>
        /// <param name="currency"> should have ID for item to Update</param>
        /// <returns>Success</returns>
        Task<bool> Update(Currency currency);
    }
}
