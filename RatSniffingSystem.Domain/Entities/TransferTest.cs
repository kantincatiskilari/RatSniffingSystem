using RatSniffingSystem.Domain.Common;
using RatSniffingSystem.Domain.Contracts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Domain.Entities
{
    public class TransferTest : ISessionLinkedEntity, IHasId
    {
        public Guid Id { get; set; }

        public Guid SessionId { get; set; }
        public Session Session { get; set; }

        public string NewOdor { get; set; } = string.Empty; // Yeni hedef koku
        public int SessionToSuccess { get; set; } // %80 Basari Orani Icin Gecen Oturum
        public bool WasSuccessful { get; set; }
        public string? Notes { get; set; }
    }
}
