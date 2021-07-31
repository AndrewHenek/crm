using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crm.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace crm.Logic.RatesSynchronization
{
    public class RatesSynchronizer
    {
        public async Task Synchronize(List<ExchangeRate> latestRates)
        {
            if (latestRates == null || !latestRates.Any())
            {
                return;
            }

            using (var context = new ApplicationDbContext())
            {
                var currenciesFromDb = await context.Currencies.Where(currency => currency.IsSync && !currency.Ghosted).ToListAsync();
                var syncItems = currenciesFromDb.Join(
                    latestRates, 
                    currency => currency.Symbol.ToUpper(), 
                    latestRate => latestRate.Symbol.ToUpper(),
                    (currency, exchangeRate) => new
                    {
                        Currency = currency,
                        ExchangeRate = exchangeRate
                    });

                foreach (var syncItem in syncItems)
                {
                    syncItem.Currency.Rate = syncItem.ExchangeRate.Rate;
                    syncItem.Currency.UpdatedAt = DateTime.Now;
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
