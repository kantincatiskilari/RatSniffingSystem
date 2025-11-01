using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

<<<<<<<< HEAD:RatSniffingSystem.Domain/Contracts/ISessionLinkedEntity.cs
namespace RatSniffingSystem.Domain.Contracts
========
namespace RatSniffingSystem.Domain.Common
>>>>>>>> d6eb9010344d8dea3b02ea8fad276e5967a31956:RatSniffingSystem.Domain/Common/ISessionLinkedEntity.cs
{
    public interface ISessionLinkedEntity
    {
        public Guid SessionId { get; set; }
    }
}
