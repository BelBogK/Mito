using Mitto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mitto.DataLayer
{
    public class DbInitialize
    {
        public static void InitializeDB(bool clearDb)
        {
            using var context = new ApplicationContext();
            if (clearDb)
                ClearData(context);

            InitCountries(context);

            context.SaveChanges();

            InitMobileOperator(context);

            context.SaveChanges();
        }

        private static void InitMobileOperator(ApplicationContext context)
        {
            MobileOperator mobileOperator;
            if (!context.MobileOperatorInfos.Any(x => x.Name == "Vodafon"))
            {
                mobileOperator = InitVodafone(context);
                context.MobileOperatorInfos.Add(mobileOperator);             
            }
            if (!context.MobileOperatorInfos.Any(x => x.Name == "TMobile"))
            {
                mobileOperator = InitTMobile(context);
                context.MobileOperatorInfos.Add(mobileOperator);
            }
            if (!context.MobileOperatorInfos.Any(x => x.Name == "PolandOperator"))
            {
                mobileOperator = InitPolandOperator(context);
                context.MobileOperatorInfos.Add(mobileOperator);
            }
        }

        private static MobileOperator InitPolandOperator(ApplicationContext context)
        {
            var mobileOperator = new MobileOperator()
            {
                Name = "PolandOperator"
            };

            var poland = context.Countries.First(x => x.Name == "Poland");
            
            mobileOperator.Countries.Add(new CountryMobileOperator() { MobileOperator=mobileOperator, MobileOperatorID=mobileOperator.Id,Country=poland, CountryId=poland.ID});

            var polandDetail = CreateMobilePriceInfo(mobileOperator, poland, 250);

            polandDetail.PricesForSMSs.Add(new PriceForSMS { AppliedDate = new DateTime(2020, 04, 01), Price = 1, MobileOperatorDetailID = polandDetail.ID });
            polandDetail.PricesForSMSs.Add(new PriceForSMS { AppliedDate = new DateTime(2020, 03, 01), Price = 1.2m, MobileOperatorDetailID = polandDetail.ID });
            polandDetail.PricesForSMSs.Add(new PriceForSMS { AppliedDate = new DateTime(2020, 02, 01), Price = 1.1m, MobileOperatorDetailID = polandDetail.ID });
            polandDetail.PricesForSMSs.Add(new PriceForSMS { AppliedDate = new DateTime(2020, 01, 01), Price = .5m, MobileOperatorDetailID = polandDetail.ID });

            mobileOperator.MobileOperatorDetails.Add(polandDetail);
             
            return mobileOperator;
        }

        private static MobileOperator InitTMobile(ApplicationContext context)
        {
            var mobileOperator = new MobileOperator()
            {
                Name = "TMobile"
            };

            var germany = context.Countries.First(x => x.Name == "Germany");
            var australiya = context.Countries.First(x => x.Name == "Austria");
            var poland = context.Countries.First(x => x.Name == "Poland");

            mobileOperator.Countries.Add(new CountryMobileOperator() { CountryId = germany.ID, Country = germany, MobileOperatorID = mobileOperator.Id, MobileOperator = mobileOperator });
            mobileOperator.Countries.Add(new CountryMobileOperator() { CountryId = australiya.ID, Country = australiya, MobileOperator = mobileOperator, MobileOperatorID = mobileOperator.Id });
            mobileOperator.Countries.Add(new CountryMobileOperator() { CountryId = poland.ID, Country = poland, MobileOperator = mobileOperator, MobileOperatorID = mobileOperator.Id });

            var mobileOD = CreateMobilePriceInfo(mobileOperator, australiya, 111);
            
            mobileOD.PricesForSMSs.Add(new PriceForSMS { AppliedDate = new DateTime(2020, 04, 01), Price = 0.2m, MobileOperatorDetailID = mobileOD.ID, OperatorDetail = mobileOD });
            mobileOD.PricesForSMSs.Add(new PriceForSMS { AppliedDate = new DateTime(2020, 03, 01), Price = 0.22m, MobileOperatorDetailID = mobileOD.ID, OperatorDetail = mobileOD });
            mobileOD.PricesForSMSs.Add(new PriceForSMS { AppliedDate = new DateTime(2020, 02, 01), Price = 0.3m, MobileOperatorDetailID = mobileOD.ID, OperatorDetail = mobileOD });
            mobileOD.PricesForSMSs.Add(new PriceForSMS { AppliedDate = new DateTime(2020, 01, 01), Price = 0.02m, MobileOperatorDetailID = mobileOD.ID, OperatorDetail = mobileOD }); 

            mobileOperator.MobileOperatorDetails.Add(mobileOD);

            mobileOD = CreateMobilePriceInfo(mobileOperator, germany, 112);
            mobileOD.PricesForSMSs.Add(new PriceForSMS { AppliedDate = new DateTime(2020, 04, 01), Price = 0.2m, MobileOperatorDetailID = mobileOD.ID, OperatorDetail = mobileOD });
            mobileOD.PricesForSMSs.Add(new PriceForSMS { AppliedDate = new DateTime(2020, 03, 01), Price = 0.2m, MobileOperatorDetailID = mobileOD.ID, OperatorDetail = mobileOD });
            mobileOD.PricesForSMSs.Add(new PriceForSMS { AppliedDate = new DateTime(2020, 02, 01), Price = 0.3m, MobileOperatorDetailID = mobileOD.ID, OperatorDetail = mobileOD });
            mobileOD.PricesForSMSs.Add(new PriceForSMS { AppliedDate = new DateTime(2020, 01, 01), Price = 0.02m, MobileOperatorDetailID = mobileOD.ID, OperatorDetail = mobileOD });

            mobileOperator.MobileOperatorDetails.Add(mobileOD);


            mobileOD = CreateMobilePriceInfo(mobileOperator, poland, 113);
            mobileOD.PricesForSMSs.Add(new PriceForSMS { AppliedDate = new DateTime(2020, 04, 01), Price = 2m, MobileOperatorDetailID = mobileOD.ID, OperatorDetail = mobileOD });
            mobileOD.PricesForSMSs.Add(new PriceForSMS { AppliedDate = new DateTime(2020, 03, 01), Price = 2.1m, MobileOperatorDetailID = mobileOD.ID, OperatorDetail = mobileOD });
            mobileOD.PricesForSMSs.Add(new PriceForSMS { AppliedDate = new DateTime(2020, 02, 01), Price = 2.2m, MobileOperatorDetailID = mobileOD.ID, OperatorDetail = mobileOD });
            mobileOD.PricesForSMSs.Add(new PriceForSMS { AppliedDate = new DateTime(2020, 01, 01), Price = 1m, MobileOperatorDetailID = mobileOD.ID, OperatorDetail = mobileOD });

            mobileOperator.MobileOperatorDetails.Add(mobileOD);
             

            return mobileOperator;
        }

        private static MobileOperator InitVodafone(ApplicationContext context)
        {
            var mobileOperator = new MobileOperator()
            {
                Name = "Vodafon"
            };

            var germany = context.Countries.First(x => x.Name == "Germany");
            var australiya = context.Countries.First(x => x.Name == "Austria");
            var all = context.Countries.ToList();
            var poland = context.Countries.First(x => x.Name == "Poland");

            mobileOperator.Countries.Add(new CountryMobileOperator() { Country = germany, CountryId = germany.ID, MobileOperator = mobileOperator, MobileOperatorID = mobileOperator.Id });
            mobileOperator.Countries.Add(new CountryMobileOperator() { Country = australiya, CountryId = australiya.ID, MobileOperator = mobileOperator, MobileOperatorID = mobileOperator.Id });
            mobileOperator.Countries.Add(new CountryMobileOperator() { Country = poland, CountryId = poland.ID, MobileOperator = mobileOperator, MobileOperatorID = mobileOperator.Id });


            var mobileOD = CreateMobilePriceInfo(mobileOperator, australiya,  232);
            
            mobileOD.PricesForSMSs.Add(new PriceForSMS { AppliedDate = new DateTime(2020, 04, 01), Price = 0.1m, MobileOperatorDetailID = mobileOD.ID, OperatorDetail = mobileOD });
            mobileOD.PricesForSMSs.Add(new PriceForSMS { AppliedDate = new DateTime(2020, 03, 01), Price = 0.11m, MobileOperatorDetailID = mobileOD.ID, OperatorDetail = mobileOD });
            mobileOD.PricesForSMSs.Add(new PriceForSMS { AppliedDate = new DateTime(2020, 02, 01), Price = 0.15m, MobileOperatorDetailID = mobileOD.ID, OperatorDetail = mobileOD });
            mobileOD.PricesForSMSs.Add(new PriceForSMS { AppliedDate = new DateTime(2020, 01, 01), Price = 0.01m, MobileOperatorDetailID = mobileOD.ID, OperatorDetail = mobileOD });
             
            mobileOperator.MobileOperatorDetails.Add(mobileOD);


            mobileOD = CreateMobilePriceInfo(mobileOperator, germany, 262);

            mobileOD.PricesForSMSs.Add(new PriceForSMS { AppliedDate = new DateTime(2020, 04, 01), Price = 0.1m, MobileOperatorDetailID = mobileOD.ID, OperatorDetail = mobileOD });
            mobileOD.PricesForSMSs.Add(new PriceForSMS { AppliedDate = new DateTime(2020, 03, 01), Price = 0.09m, MobileOperatorDetailID = mobileOD.ID, OperatorDetail = mobileOD });
            mobileOD.PricesForSMSs.Add(new PriceForSMS { AppliedDate = new DateTime(2020, 02, 01), Price = 0.15m, MobileOperatorDetailID = mobileOD.ID, OperatorDetail = mobileOD });
            mobileOD.PricesForSMSs.Add(new PriceForSMS { AppliedDate = new DateTime(2020, 01, 01), Price = 0.01m, MobileOperatorDetailID = mobileOD.ID, OperatorDetail = mobileOD });

            mobileOperator.MobileOperatorDetails.Add(mobileOD);

            mobileOD = CreateMobilePriceInfo(mobileOperator, germany, 292);

            mobileOD.PricesForSMSs.Add(new PriceForSMS { AppliedDate = new DateTime(2020, 04, 01), Price = 0.15m, MobileOperatorDetailID = mobileOD.ID, OperatorDetail = mobileOD });
            mobileOD.PricesForSMSs.Add(new PriceForSMS { AppliedDate = new DateTime(2020, 03, 01), Price = 0.04m, MobileOperatorDetailID = mobileOD.ID, OperatorDetail = mobileOD });
            mobileOD.PricesForSMSs.Add(new PriceForSMS { AppliedDate = new DateTime(2020, 02, 01), Price = 0.19m, MobileOperatorDetailID = mobileOD.ID, OperatorDetail = mobileOD });
            mobileOD.PricesForSMSs.Add(new PriceForSMS { AppliedDate = new DateTime(2020, 01, 01), Price = 0.0001m, MobileOperatorDetailID = mobileOD.ID, OperatorDetail = mobileOD });

            mobileOperator.MobileOperatorDetails.Add(mobileOD);


            mobileOD = CreateMobilePriceInfo(mobileOperator, poland, 260);

            mobileOD.PricesForSMSs.Add(new PriceForSMS { AppliedDate = new DateTime(2020, 04, 01), Price = 0.1m, MobileOperatorDetailID = mobileOD.ID, OperatorDetail = mobileOD });
            mobileOD.PricesForSMSs.Add(new PriceForSMS { AppliedDate = new DateTime(2020, 03, 01), Price = 1.1m, MobileOperatorDetailID = mobileOD.ID, OperatorDetail = mobileOD });
            mobileOD.PricesForSMSs.Add(new PriceForSMS { AppliedDate = new DateTime(2020, 02, 01), Price = 1.2m, MobileOperatorDetailID = mobileOD.ID, OperatorDetail = mobileOD });
            mobileOD.PricesForSMSs.Add(new PriceForSMS { AppliedDate = new DateTime(2020, 01, 01), Price = 0.5m, MobileOperatorDetailID = mobileOD.ID, OperatorDetail = mobileOD });

            mobileOperator.MobileOperatorDetails.Add(mobileOD); 

            return mobileOperator;
        }

        private static MobileOperatorDetail CreateMobilePriceInfo(MobileOperator mobileOperator, Country country, int mobileCodeCountry)
        {
            return new MobileOperatorDetail()
            {
                MobileCountryCode = mobileCodeCountry,
                Country = country,
                CountryID = country.ID,
                MobileOperatorID = mobileOperator.Id,
                MobileOperator = mobileOperator,
                PricesForSMSs = new List<PriceForSMS>()
            };
        }

        private static void InitCountries(ApplicationContext context)
        {
            Country country = null;
            if (!context.Countries.Any(x => x.Name == "Germany"))
            {
                country = AddGermany(context);
            }
            Currency currencyEUR = null;
            if (!context.Currencies.Any(x => x.CurrencyName == "EUR"))
            {
                currencyEUR = InitGermanyCurrency(context, country);
            }

            if (!context.Countries.Any(x => x.Name == "Austria"))
            {
                AddAustria(context, currencyEUR);
            }

            if (!context.Countries.Any(x => x.Name == "Poland"))
            {
                country = AddPoland(context);
            }
        }

        private static Country AddGermany(ApplicationContext context)
        {
            Country country = new Country()
            {
                CountryCode = 49,
                Name = "Germany",
            };
            context.Countries.Add(country);
            return country;
        }

        private static Currency InitGermanyCurrency(ApplicationContext context, Country country)
        {
            Currency currencyEUR = new Currency()
            {
                IsPrime = true,
                CurrencyName = "EUR",
                CurrencySymbol = "€"
            };
            country.Currency = currencyEUR;
            var exchange = new CurrencyExchange() { CurrencyID = currencyEUR.ID, AppliedDate = DateTime.MinValue, Currency = currencyEUR, Rate = 1 };
            context.Currencies.Add(currencyEUR);
            context.CurrencyExchanges.Add(exchange);
            return currencyEUR;
        }

        private static void AddAustria(ApplicationContext context, Currency currencyEUR)
        {
            context.Add(new Country()
            {
                CountryCode = 43,
                Name = "Austria",
                Currency = currencyEUR
            });
        }

        private static Country AddPoland(ApplicationContext context)
        {
            Country country = new Country()
            {
                CountryCode = 48,
                Name = "Poland"
            };
            if (!context.Currencies.Any(x => x.CurrencyName == "PLN"))
            {
                var currency = new Currency()
                {
                    IsPrime = false,
                    CurrencyName = "PLN",
                    CurrencySymbol = "zł"
                };

                country.Currency = currency;
                var exchange = new CurrencyExchange() { CurrencyID = currency.ID, AppliedDate = new DateTime(2020, 04, 01), Currency = currency, Rate = 0.22m };
                context.CurrencyExchanges.Add(exchange);
                exchange = new CurrencyExchange() { CurrencyID = currency.ID, AppliedDate = new DateTime(2020, 03, 05), Currency = currency, Rate = 0.20m };
                context.CurrencyExchanges.Add(exchange);
                exchange = new CurrencyExchange() { CurrencyID = currency.ID, AppliedDate = new DateTime(2020, 01, 05), Currency = currency, Rate = 0.10m };
                context.CurrencyExchanges.Add(exchange);
            }
            context.Countries.Add(country);
            return country;
        }

        private static void ClearData(ApplicationContext context)
        {
            //context.Database.EnsureDeleted();
            context.Countries.RemoveRange(context.Countries);
            context.Currencies.RemoveRange(context.Currencies);
            context.CurrencyExchanges.RemoveRange(context.CurrencyExchanges);
            context.MobileOperatorInfos.RemoveRange(context.MobileOperatorInfos);
            context.MobileOperatorDetails.RemoveRange(context.MobileOperatorDetails);
            context.SMSs.RemoveRange(context.SMSs);
            context.SaveChanges();
        }
    }
}
