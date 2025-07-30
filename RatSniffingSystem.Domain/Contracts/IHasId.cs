using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Domain.Contracts
{
    public interface IHasId
    {
        /// <summary>
        /// Entity'nin benzersiz ID'si
        /// </summary>
        Guid Id { get; set; }
    }
}
