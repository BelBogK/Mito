using Microsoft.EntityFrameworkCore;
using Mitto.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mitto.DataLayer
{
    public class ApplicationContext : DbContext
    {
        #region public propertyes 
        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CountryMobileOperator> CountryMobileOperators { get; set; }
        public DbSet<CurrencyExchange> CurrencyExchanges { get; set; }
        public DbSet<MobileOperator> MobileOperatorInfos { get; set; }
        public DbSet<MobileOperatorDetail> MobileOperatorDetails { get; set; }
        public DbSet<PriceForSMS> PricesFroSMS { get; set; }
        public DbSet<SMS> SMSs { get; set; }
        #endregion

        #region constructors
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        #endregion

        #region override
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //TODO: Move connection string to outside
            optionsBuilder.UseMySql("server=localhost;UserId=root;Password=MySql;database=Mitto;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CountryMobileOperator>().HasKey(c => new { c.CountryId, c.MobileOperatorID });
        }
        #endregion

    }
}
