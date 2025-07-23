using RatSniffingSystem.Domain.Entities;
using RatSniffingSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Application.DTOs
{
    public class TrialDto
    {
        public Guid Id { get; set; }
        public int TrialNumber { get; set; }
        public string TargetOdor { get; set; } = string.Empty;
        public DateTime FirstResponseTime { get; set; }
        public DateTime? FirstCorrectTime { get; set; }
        public bool IsCorrectPositive { get; set; }
        public bool IsFalsePositive { get; set; }
        public bool IsCorrectNegative { get; set; }
        public bool IsFalseNegative { get; set; }
        public List<TrialOdorDto> OdorPositions { get; set; } = new();
    }
}
