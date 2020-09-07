using Microsoft.EntityFrameworkCore;
using Mitto.Model;
using Mitto.Model.DTO;
using Motto.IDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitto.DataLayer.Repositorys
{
    public class CountryRepository : ICountryRepository
    {
        #region private members

        private readonly ApplicationContext _context;
        private readonly ICurrencyExchangeRepositroy _currencyExchangeRepositroy;
        private readonly IMobileOperatorDetailRepository _mobilePriceRepository;
        private readonly IPriceForSmsRepository _priceForSmsRepository;
        private bool _isDisposed=false;

        #endregion

        #region constructors

        public CountryRepository(ApplicationContext context, ICurrencyExchangeRepositroy currencyExchangeRepositroy, IMobileOperatorDetailRepository mobilePriceInfoRepository, IPriceForSmsRepository priceForSmsRepository)
        {
            _context = context;
            _currencyExchangeRepositroy = currencyExchangeRepositroy;
            _mobilePriceRepository = mobilePriceInfoRepository;
            _priceForSmsRepository = priceForSmsRepository;
        }
         
        ~CountryRepository()
        {
            if (!_isDisposed)
                Dispose();
        }
        #endregion

        #region ICountryRepository
        public async Task<IEnumerable<CountryDTO>> GetCountryInfo()
        {
            var countries =await _context.Countries.Include(x=>x.Currency).ToListAsync();

            var currency = countries.Select(async x => await _currencyExchangeRepositroy.GetLastCurrencyExForCountry(x))
                .Select(r => r.Result)
                .Where(rc => rc != null).ToList();

            var result = countries.Select(async x => new CountryDTO
            {
                Name = x.Name,
                CC = x.CountryCode.ToString(),
                OperatorPrices =(await _mobilePriceRepository.GetForCountry(x.ID)).Select(async mop => new MobileOperatorDTO
                {
                    MCC = mop.MobileCountryCode,
                    MobileOperatorName = mop.MobileOperator.Name,
                    PricePerSMS = currency.First(x => x.Currency.ID == x.Currency.ID).Rate * (await _priceForSmsRepository.GetPriceForSMS(mop.ID)).Price
                }).Select(i=>i.Result).ToList()
            }).Select(r=>r.Result).ToList();

            return  result;
        }

        public async Task<List<int>> GetAllCountriesCode()
        {
            return await _context.Countries.Select(x => x.CountryCode).ToListAsync();
        }

        //TODO: you can use firsrOrDefault for more perfomance 
        public async Task<int> GetCountryIDByCountryCode(int countryCode)
        {
            var result =await _context.Countries.SingleOrDefaultAsync(x => x.CountryCode == countryCode);
            
            if (result == null)
                return -1;

            return result.ID;
        }

        public async Task<Country> GetCountryByName(string name)
        {
            return await _context.Countries.FirstAsync(x => x.Name == name);
        }

        public async Task<Country> GetCountryByCountryCode(string countryCode)
        {
            return await _context.Countries.FirstAsync(x => x.CountryCode == int.Parse(countryCode));
        }

        #endregion

        #region #IDisposable
        public void Dispose()
        {
            
            _context.Dispose();
            _isDisposed = true;
        }
        #endregion
    }
}
