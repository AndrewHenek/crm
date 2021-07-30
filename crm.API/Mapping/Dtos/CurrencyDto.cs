using System;
using System.ComponentModel.DataAnnotations;

namespace crm.API.Mapping.Dtos
{
    /// <summary>
    /// Currency
    /// </summary>
    public class CurrencyDto
    {
        /// <summary>
        /// Currency ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Currency symbol
        /// </summary>
        [Required]
        public string Symbol { get; set; }

        /// <summary>
        /// Currency rate
        /// </summary>
        public double? Rate { get; set; }

        /// <summary>
        /// Date of currency update 
        /// </summary>
        public DateTime Updated_at { get; set; }
    }
}
