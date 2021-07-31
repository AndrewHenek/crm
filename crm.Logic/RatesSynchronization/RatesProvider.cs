using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace crm.Logic.RatesSynchronization
{
    public class RatesProvider
    {
        private const string Url = "https://api.exchangerate.host/latest?base=USD";

        public async Task<List<ExchangeRate>> GetLatestRates()
        {
            var client = new HttpClient();
            var currentCurrenciesTask = client.GetStringAsync(Url);
            var result = await currentCurrenciesTask;
            var jsonResult = JsonConvert.DeserializeObject<ExchangeRates>(result);

            if (jsonResult == null)
            {
                return new List<ExchangeRate>();
            }

            var currentRates = jsonResult.Rates.Properties().Select(property => new ExchangeRate
            {
                Symbol = property.Name,
                Rate = double.Parse(property.Value.ToString().Replace(',', '.'), CultureInfo.InvariantCulture)
            }).ToList();

            return currentRates;
        }
    }
}
