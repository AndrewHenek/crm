using System;
using System.Threading.Tasks;
using crm.Logic.RatesSynchronization;

namespace crm.Synchronization
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var ratesProvider = new RatesProvider();
                var latestRates = await ratesProvider.GetLatestRates();
                var ratesSynchronizer = new RatesSynchronizer();
                await ratesSynchronizer.Synchronize(latestRates);
                Console.WriteLine("Synchronizacja zakończona pomyślnie");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Podczas synchronizacji danych wystąpił błąd {e.Message}");
                Console.WriteLine(e);
            }
        }
    }
}
