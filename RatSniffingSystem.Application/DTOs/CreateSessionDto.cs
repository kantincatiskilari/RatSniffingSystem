using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Application.DTOs
{
    public class CreateSessionDto
    {
        public DateTime Date { get; set; }
        public int DurationMinutes { get; set; }
        public string CageCode { get; set; } = string.Empty;
        public string? MaterialType { get; set; }
        public DateTime? MaterialThawedAt { get; set; }
        public DateTime StartTime { get; set; }  

        public Guid RatId { get; set; }
        public Guid TrainerId { get; set; }
    }
}
