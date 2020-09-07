using Microsoft.EntityFrameworkCore;
using Mitto.Model;
using Motto.IDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitto.DataLayer.Repositorys
{
    public class CurrencyExchangeRepositroy : ICurrencyExchangeRepositroy
    {
        #region private members

        private ApplicationContext _context;

        #endregion

        #region Constructors

        public CurrencyExchangeRepositroy(ApplicationContext context) => _context = context;

        #endregion

        #region ICurrencyExchangeRepositroy

        public async Task<List<CurrencyExchengeApplied>> GetCurrencyExForPeriod(int currencyID, DateTime startPeriod, DateTime endPeriod)
        {
            var startDate = _context.CurrencyExchanges
                .Where(x => x.CurrencyID == currencyID && x.AppliedDate <= startPeriod).Max(ad => ad.AppliedDate);

            return await _context.CurrencyExchanges
                .Where(x => x.AppliedDate >= startDate && x.AppliedDate <= endPeriod)
                .Select(cea => new CurrencyExchengeApplied() { AppliedDate = cea.AppliedDate, Rate = cea.Rate })
                .ToListAsync();

        }

        public async Task<CurrencyExchange> GetLastCurrencyExForCountry(Country country)
        { 
            var result = _context.CurrencyExchanges
                .OrderByDescending(d => d.AppliedDate)
                .FirstAsync(ce => ce.Currency.ID == country.CurrencyID);

            return await result;
        }

        public async Task<CurrencyExchange> GetCurrencyExchange(int currencyID, DateTime time)
        {
            var result= await _context.CurrencyExchanges.Where(x => x.CurrencyID == currencyID && x.AppliedDate <= time).OrderByDescending(a => a.AppliedDate).FirstAsync();
            if (result == null)
                //TODO: Create own exception class
                throw new KeyNotFoundException($"Can't find currency exchenge for {currencyID} with period {time}");
            return result;
        }
        #endregion
    }
}
