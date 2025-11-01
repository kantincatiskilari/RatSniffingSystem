using RatSniffingSystem.Domain.Common;
using RatSniffingSystem.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Domain.Entities
{
    public class Trial : ISessionLinkedEntity, IHasId
    {
        public Guid Id { get; set; }

        public Guid SessionId { get; set; }
        public Session Session { get; set; }

        public int TrialNumber { get; set; }
        public string TargetOdor { get; set; } = string.Empty;
        public List<TrialOdor> OdorPositions { get; set; } = new ();
        public DateTime FirstResponseTime { get; set; }
        public DateTime? FirstCorrectTime { get; set; }
        public bool IsCorrectPositive { get; set; }
        public bool IsFalsePositive { get; set; }
        public bool IsCorrectNegative { get; set; }
        public bool IsFalseNegative { get; set; }

    }
}
