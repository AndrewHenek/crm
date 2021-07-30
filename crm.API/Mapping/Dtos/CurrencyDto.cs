using System;

namespace crm.API.Mapping.Dtos
{
    public class CurrencyDto
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public double? Rate { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
