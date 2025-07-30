using RatSniffingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Contracts.Interfaces
{
    public interface IOdorService<TDto, TCreateDto> : IGenericService<TDto, TCreateDto, object>
        where TCreateDto : class
        where TDto : class
    {
        /// <summary>
        /// Belirli bir isme sahip kayitlari getirir
        /// </summary>
        Task<TDto?> GetByNameAsync(string name);

        /// <summary>
        /// Belirli bir kategorideki kokulari getirir
        /// </summary>

        Task<List<TDto?>> GetByCategoryAsync(OdorCategory category);

        /// <summary>
        /// Belirli bir dissal koda sahip kayitlari getirir
        /// </summary>
        Task<TDto?> GetByExternalCodeAsync(string externalCode);

        /// <summary>
        /// Kokunun olumcul olup olmamasina gore kayitlari getirir
        /// </summary>
        Task<TDto?> GetByHazardousStatusAsync(bool isHazardous);

    }
}
