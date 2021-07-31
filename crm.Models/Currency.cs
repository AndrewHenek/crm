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
        [Remote("SymbolIsUnique", "Currencies", ErrorMessage = "Symbol already exists.", AdditionalFields = nameof(Id))]
        public string Symbol { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public double? Rate { get; set; }

        [Display(Name = "Is sync")]
        public bool IsSync { get; set; }

        [Display(Name = "Creation date")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Modification date")]
        public DateTime UpdatedAt { get; set; }

        [Display(Name = "Archive")]
        public bool Ghosted { get; set; }
    }
}