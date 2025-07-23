using RatSniffingSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Application.DTOs
{
    public class FoodIntakeLogDto
    {
        public Guid Id { get; set; }

        public double AmountInCc { get; set; }
        public DateTime? TimeGiven { get; set; }
        public string Notes { get; set; }
    }
}
