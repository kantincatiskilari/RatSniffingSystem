using RatSniffingSystem.Domain.Contracts;
using RatSniffingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Domain.Entities
{
    public class Intervention : ISessionLinkedEntity, IHasId
    {
        public Guid Id { get; set; }

        public Guid SessionId { get; set; }
        public Session Session { get; set; } = null!;

        public string Substance { get; set; } = string.Empty;
        public string Dose { get; set; } = string.Empty;
        public DateTime AppliedAt { get; set; }
        public InterventionType InterventionType { get; set; } = InterventionType.Drug;
        public string? Notes { get; set; }
    }
}
