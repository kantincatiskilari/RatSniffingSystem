using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Application.DTOs
{
    public class UpdateSessionDto
    {
        public Guid Id { get; set; }
        public int DurationMinutes { get; set; }
        public string CageCode { get; set; } = string.Empty;

        public TimeSpan StartTime { get; set; }
        public string? MaterialType { get; set; }
        public TimeSpan? MaterialThawedAt { get; set; }
    }
}
