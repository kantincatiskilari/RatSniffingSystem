using RatSniffingSystem.Domain.Contracts;
using RatSniffingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Contracts.Interfaces
{
    public interface IOdorService<TEntity, TDto, TCreateDto> : IGenericService<TEntity, TDto, TCreateDto, object>
        where TEntity : class, IHasId
        where TCreateDto : class
        where TDto : class
    {
        /// <summary>
        /// Belirli bir isme sahip kayitlari getirir
        /// </summary>
        Task<IReadOnlyList<TDto?>> GetByNameAsync(string name);

        /// <summary>
        /// Belirli bir kategorideki kokulari getirir
        /// </summary>

        Task<IReadOnlyList<TDto>> GetByCategoryAsync(OdorCategory category, CancellationToken ct);

        /// <summary>
        /// Belirli bir dissal koda sahip kayitlari getirir
        /// </summary>
        Task<TDto?> GetByExternalCodeAsync(string externalCode);

        /// <summary>
        /// Kokunun olumcul olup olmamasina gore kayitlari getirir
        /// </summary>
        Task<IReadOnlyList<TDto?>> GetByHazardousStatusAsync(bool isHazardous);

    }
}
