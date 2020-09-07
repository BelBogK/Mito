using Mitto.Model.DTO;
using Mitto.Web.ServicesDTOs;
using Motto.IDataLayer;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mitto.Web.Services
{
    public class CountriesService: Service
    {
        #region private members

        private ICountryRepository _countryRepository;

        #endregion

        #region constructor

        public CountriesService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        #endregion
        public async Task<IEnumerable<CountryDTO>> Get(CountriesDTOService tdo)
        {
            return await _countryRepository.GetCountryInfo();
        }
    }
}
