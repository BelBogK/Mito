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
    public class MobileOperatorDetailRepository : IMobileOperatorDetailRepository
    {
        #region private members

        private ApplicationContext _context;

        #endregion

        #region contructors
        public MobileOperatorDetailRepository(ApplicationContext context)
        {
            _context = context;
        } 

        #endregion

        #region IMobilePriceInfoRepository

        public async Task<List<MobileOperatorDetail>> GetForCountry(int countryId)
        {

            var resutl = _context.MobileOperatorDetails
                .Where(x => x.CountryID == countryId).Include(i=>i.MobileOperator).ToListAsync(); 

            return  await resutl;
        }

        public async Task<List<int>> GetAllMobileOperatorMobileCode()
        {
            return await _context.MobileOperatorDetails.Select(x => x.MobileCountryCode).ToListAsync();
        }

        public async Task<MobileOperatorDetail> GetByMobileCode(int mobileCode)
        {
            return await _context.MobileOperatorDetails.FirstAsync(x => x.MobileCountryCode == mobileCode);
        }

        #endregion
    }
}
