using Microsoft.EntityFrameworkCore;
using Mitto.Model;
using Mitto.Model.DTO;
using Motto.IDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mitto.DataLayer.Repositorys
{
    public class SMSRepository: ISMSRepository
    {
        #region private members

        private ApplicationContext _context;
        private IMobileOperatorDetailRepository _mobileOperatorDetailRepository;
        private ICountryRepository _countryRepository;
        private IPriceForSmsRepository _priceForSmsRepository;
        ICurrencyExchangeRepositroy _currencyExchangeRepositroy;
        #endregion

        #region constructors

        public SMSRepository(ApplicationContext context, IMobileOperatorDetailRepository mobileOperatorDetailRepository, ICountryRepository countryRepository, IPriceForSmsRepository priceForSmsRepository, ICurrencyExchangeRepositroy currencyExchangeRepositroy)
        {
            _context = context;
            _mobileOperatorDetailRepository= mobileOperatorDetailRepository;
            _countryRepository = countryRepository;
            _priceForSmsRepository = priceForSmsRepository;
            _currencyExchangeRepositroy = currencyExchangeRepositroy;
        }

        #endregion

        #region ISMSRepository
        public async Task<bool> Save(SMS sms)
        {
            _context.SMSs.Add(sms);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<IEnumerable<SMS>> GetAllMessageSendedBy(PhoneInfo pi)
        {
            return await _context.SMSs.Where(sms => sms.FromCountryCode == pi.CountryCode
            && sms.FromMobileCode == pi.MobileCode
            && sms.FromNumber == pi.PhoneNumber)
                .ToListAsync();
        }

        public async Task<int> GetCountOfSMS()
        {
            return await _context.SMSs.CountAsync();
        }

        public async Task<SMS> GetLastSMS()
        {
            return await _context.SMSs.OrderBy(sms => sms.Sended).FirstAsync();
        }

        //TODO: Need implement algorithm when recipient and sender in different country; (I think it's should be difference price) 
        public async Task<SentedSMSConteinerDTO> GetSentedSMS(DateTime from, DateTime to, int take = 10, int skip = 0)
        {
            var result = new SentedSMSConteinerDTO();

            var querry = _context.SMSs.Where(x => x.Sended >= from && x.Sended <= to);
            var count =await querry.CountAsync();
            var smses = await querry
                .Skip(skip).Take(take)
                .ToListAsync();

            result.Count =count;
            if (count == 0)
                return result;

            var container = GetContainerInfoForSMS(from, to, smses);

            //TODO: rewrite that to cache 
            result.Items = smses.Select(async x => new SentedSMSDTO
            {
                dateTime = x.Sended,
                from = x.FromPhoneNumber,
                to = x.ToPhoneNumber,
                mcc = x.ToMobileCode,
                price = await CalculatePriceForSMS(new SMSSentedInfo(x, container.MobileOperators[x.FromCountryCode].ID), container)
            }).Select(r => r.Result).ToList();            

            return result;
        }

     


        public IEnumerable<SMSStatisticTDO> GetStatistic(DateTime from, DateTime to, IEnumerable<string> mccList)
        {
            IEnumerable<SMS> sms;
            if(mccList.Any())
            {
                sms = _context.SMSs.Where(x => mccList.Contains(x.FromMobileCode));
            }
            else
            {
                sms = _context.SMSs;
            }
            sms = sms.Where(x => x.Sended >= from && x.Sended <= to);

            var container = GetContainerInfoForSMS(from, to, sms.ToList());
            var smses = sms.GroupBy(x => new { Date = x.Sended, MobileCode = x.FromMobileCode, CountryCode = x.FromCountryCode });

            var result = smses.Select(async x => new SMSStatisticTDO
            {
                Date = x.Key.Date,
                Mcc = x.Key.MobileCode,
                Count = x.Count(),
                PricePerSms = await CalculatePriceForSMS(
                    new SMSSentedInfo() { Sented = x.Key.Date, CountryCode = x.Key.CountryCode, MobileOperatorDetailID = container.MobileOperators[x.Key.CountryCode].ID }, container)
            }).Select(x=>x.Result);

            return result.ToList();
        }

        #endregion

        #region private methods

        private ContainerForSMSInfo GetContainerInfoForSMS(DateTime from, DateTime to, IEnumerable<SMS> smses)
        {
            var result = new ContainerForSMSInfo();

            //TODO: I Selected FromCountryCode becoause we pay to sender company
            result.Countries = smses.Select(x => x.FromCountryCode).Distinct().ToDictionary(x => x, x => _countryRepository.GetCountryByCountryCode(x).Result);
            result.MobileOperators = result.Countries.ToDictionary(k => k.Key, k => _mobileOperatorDetailRepository.GetByMobileCode(int.Parse(smses.First(x => x.FromCountryCode == k.Key).FromMobileCode)).Result);
            result.priceForSMSes = result.MobileOperators.ToDictionary(k => k.Key, k => _priceForSmsRepository.GetPriceForSMS(k.Value.MobileOperatorID, from, to).Result);

            return result;
        }

        private async Task<decimal> CalculatePriceForSMS(SMSSentedInfo sms, ContainerForSMSInfo container)
        {
            PriceForSMS priceForSMS;
            decimal rate;

            priceForSMS = container.priceForSMSes[sms.CountryCode]
                                 .Where(p => p.AppliedDate <= sms.Sented && p.MobileOperatorDetailID == sms.MobileOperatorDetailID)
                                 .OrderByDescending(a => a.AppliedDate)
                                 .FirstOrDefault();
            if (priceForSMS == null)
                throw new KeyNotFoundException($"Can't find price for sms with mobile operator {sms.MobileOperatorDetailID} for {sms.Sented}");

            rate = (await _currencyExchangeRepositroy.GetCurrencyExchange(container.Countries[sms.CountryCode].CurrencyID, sms.Sented)).Rate;

            return priceForSMS.Price * rate;
        }



        #endregion

        #region IDisposable
        public void Dispose()
        {
            _context.Dispose();
        }
        #endregion

        #region partial classes

        class ContainerForSMSInfo
        {
            public Dictionary<string,Country> Countries { get; set; }
            public Dictionary<string,MobileOperatorDetail> MobileOperators { get; set; }
            public Dictionary<string,IEnumerable<PriceForSMS>> priceForSMSes { get; set; }
        }

        class SMSSentedInfo
        {
            #region public properties
            public DateTime Sented { get; set; }
            public string CountryCode { get; set; }
            public int MobileOperatorDetailID { get; set; }
            #endregion

            #region constructors

            public SMSSentedInfo()
            {

            }

            public SMSSentedInfo(SMS sMS, int mobileDetatilID)
            {
                Sented = sMS.Sended;
                CountryCode = sMS.FromCountryCode;
                MobileOperatorDetailID = mobileDetatilID;
            }

            #endregion


        }

        #endregion
    }
}
