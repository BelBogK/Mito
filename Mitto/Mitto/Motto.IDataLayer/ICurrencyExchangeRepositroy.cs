using Mitto.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Motto.IDataLayer
{
    public interface ICurrencyExchangeRepositroy
    {
        Task<CurrencyExchange> GetLastCurrencyExForCountry(Country country);
        Task<List<CurrencyExchengeApplied>> GetCurrencyExForPeriod(int currencyID, DateTime startPeriod, DateTime endPeriod);
        Task<CurrencyExchange> GetCurrencyExchange(int currencyID, DateTime time);
    }
}
