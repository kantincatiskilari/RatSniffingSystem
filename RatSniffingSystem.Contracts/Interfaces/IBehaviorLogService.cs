
using RatSniffingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Contracts.Interfaces
{
    public interface IBehaviorLogService<TDto, TCreateDto> : IGenericService<TDto, TCreateDto, object>
        where TCreateDto : class
        where TDto : class
    {
        /// <summary>
        /// Belirli bir davranis etiketine sahip sicanlari getirir
        /// </summary>

        Task<List<TDto>> GetByBehaviorTypeAsync(BehaviorType type);
    }
}
