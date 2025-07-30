using RatSniffingSystem.Domain.Contracts;
using RatSniffingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Contracts.Interfaces
{
    public interface IRatWeightService<TEntity, TDto, TCreateDto> : IGenericService<TEntity, TDto, TCreateDto, object>
        where TEntity : class, IHasId
        where TCreateDto : class
        where TDto : class
    {
        /// <summary>
        /// Belirli bir kilo araligina sahip hayvani getirir
        /// </summary>

        Task<List<TDto>> GetByWeightRangeAsync(decimal firstGram, decimal LastGram);
    }
}
