using RatSniffingSystem.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Domain.Entities
{
    public class FoodIntakeLog : ISessionLinkedEntity, IHasId
    {
        public Guid Id { get; set; }

        public Guid SessionId { get; set; }
        public Session Session { get; set; } = null!;

        public double AmountInCc { get; set; }
        public DateTime? TimeGiven { get; set; }
        public string Notes { get; set; }
    }
}
