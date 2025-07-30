using RatSniffingSystem.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Domain.Entities
{
    public class ExperimentalNote : ISessionLinkedEntity, IHasId
    {
        public Guid Id { get; set; }

        public Guid SessionId { get; set; }
        public Session Session { get; set; } = null!;

        public string Title { get; set; } = string.Empty; 
        public string? Content { get; set; }              
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
