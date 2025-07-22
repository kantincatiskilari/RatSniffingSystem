using RatSniffingSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Domain.Entities
{
    public class Trial
    {
        public Guid Id { get; set; }

        public Guid SessionId { get; set; }
        public Session Session { get; set; }

        public int TrialNumber { get; set; }
        public string TargetOdor { get; set; } = string.Empty;
        public List<string> DistractorOdors { get; set; } = new();
        public List<OdorPosition> OdorPositions { get; set; }
        public TimeSpan FirstResponseTime { get; set; }
        public TimeSpan? FirstCorrectTime { get; set; }
        public bool IsCorrectPositive { get; set; }
        public bool IsFalsePositive { get; set; }
        public List<string> FalsePositiveOdors { get; set; }
        public bool IsCorrectNegative { get; set; }
        public bool IsFalseNegative { get; set; }
    }
}
