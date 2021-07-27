﻿using System;

namespace crm.Models
{
    public class Currency
    {
        public int Id { get; set; }

        public string Symbol { get; set; }

        public string Name { get; set; }

        public double Rate { get; set; }

        public bool IsSync { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool Ghosted { get; set; }
    }
}