
using RatSniffingSystem.Domain.Contracts;
using RatSniffingSystem.Domain.Entities;
using RatSniffingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Contracts.Interfaces
{
    public interface IBehaviorLogService<TEntity, TDto, TCreateDto> : IGenericService<TEntity, TDto, TCreateDto, object>
        where TEntity : class, IHasId
        where TCreateDto : class
        where TDto : class
    {
        /// <summary>
        /// Belirli bir davranis etiketine sahip sicanlari getirir
        /// </summary>

        Task<IReadOnlyList<TDto>> GetByBehaviorTypeAsync(BehaviorType type, CancellationToken ct = default);
    }
}
