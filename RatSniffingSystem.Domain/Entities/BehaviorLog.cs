using RatSniffingSystem.Domain.Entity;
using RatSniffingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Domain.Entities
{
    public class BehaviorLog
    {
        public Guid Id { get; set; }

        public Guid SessionId { get; set; }
        public Session Session { get; set; } = null!;

        public BehaviorType BehaviorType { get; set; }
        public DateTime TimeObserved { get; set; }
        public string? Notes { get; set; }
    }
}
