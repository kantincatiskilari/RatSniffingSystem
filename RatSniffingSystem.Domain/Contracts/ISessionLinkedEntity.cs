using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Domain.Contracts
{
    public interface ISessionLinkedEntity
    {
        public Guid SessionId { get; set; }
    }
}
