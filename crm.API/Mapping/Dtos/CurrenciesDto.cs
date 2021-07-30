using System.Collections.Generic;

namespace crm.API.Mapping.Dtos
{
    /// <summary>
    /// Container for the list of currencies
    /// </summary>
    public class CurrenciesDto
    {
        /// <summary>
        /// List od currencies
        /// </summary>
        public List<CurrencyDto> Currencies { get; set; }
    }
}
