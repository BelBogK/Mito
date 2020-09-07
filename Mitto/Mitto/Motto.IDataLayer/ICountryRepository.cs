using Mitto.Model;
using Mitto.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Motto.IDataLayer
{
    public interface ICountryRepository:IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Base information about country. Returned last currency </returns>
        Task<IEnumerable<CountryDTO>> GetCountryInfo();
        Task<List<int>> GetAllCountriesCode(); 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns>if not found return -1</returns>
        Task<int> GetCountryIDByCountryCode(int countryCode);
        Task<Country> GetCountryByName(string name);
        Task<Country> GetCountryByCountryCode(string countryCode);
    }
}
