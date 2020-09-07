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
    public class PriceForSmsRepository : IPriceForSmsRepository
    {
        #region private members

        private ApplicationContext _context;

        #endregion

        #region constructors

        public PriceForSmsRepository(ApplicationContext context)
        {
            _context = context;
        }

        #endregion

        #region IPriceForSmsRepository
        public async Task<PriceForSMS> GetPriceForSMS(int mobileOperatorDetailID)
        {
            var result= await _context.PricesFroSMS.Where(x => x.MobileOperatorDetailID == mobileOperatorDetailID).OrderByDescending(d => d.AppliedDate).FirstAsync();
            return result;
        }

        public async Task<IEnumerable<PriceForSMS>> GetPriceForSMS(int mobileOperatorDetailID, DateTime startTime, DateTime endTime)
        {
                var items = _context.PricesFroSMS.Where(x => x.AppliedDate <= startTime && x.MobileOperatorDetailID == mobileOperatorDetailID);
                var startDate = items.Any() ? items.Min(x => x.AppliedDate) : startTime;
                return await _context.PricesFroSMS.Where(x => x.AppliedDate >= startDate && x.AppliedDate < endTime).ToListAsync();
        }
        #endregion
    }
}
