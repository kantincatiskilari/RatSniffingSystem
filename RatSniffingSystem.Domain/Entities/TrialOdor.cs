using RatSniffingSystem.Domain.Contracts;
using RatSniffingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Domain.Entities
{
    public class TrialOdor : IHasId
    {
        public Guid Id { get; set; }

        public Guid TrialId { get; set; }
        public Trial Trial { get; set; }

        public Guid OdorId { get; set; }
        public Odor Odor { get; set; }

        public bool IsFalsePositive { get; set; } = false;

        public OdorType OdorType { get; set; }
        public int PositionIndex { get; set; }
    }
}
