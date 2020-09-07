using Mitto.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Motto.IDataLayer
{
    public interface IMobileOperatorDetailRepository
    {
        Task<List<MobileOperatorDetail>> GetForCountry(int countryID);

        Task<List<int>> GetAllMobileOperatorMobileCode();

        Task<MobileOperatorDetail> GetByMobileCode(int mobileCode);
    }
}
