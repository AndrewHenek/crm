using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace crm.Models
{
    public class Currency
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(3)]
        [Remote("SymbolIsUnique", "Currencies", ErrorMessage = "Symbol already exists.")]
        public string Symbol { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public double? Rate { get; set; }

        public bool IsSync { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool Ghosted { get; set; }
    }
}