using Newtonsoft.Json.Linq;

namespace crm.Logic.RatesSynchronization
{
    public class ExchangeRates
    {
        public bool Success { get; set; }
        public JObject Rates { get; set; }
    }
}
