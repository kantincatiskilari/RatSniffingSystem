using RatSniffingSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Domain.Entities
{
    public class RatWeight
    {
        public Guid Id { get; set; }

        public Guid RatId { get; set; }
        public Rat Rat { get; set; }

        public DateTime Date { get; set; }
        public decimal WeightInGrams { get; set; }
        public string? Notes { get; set; }
    }
}
