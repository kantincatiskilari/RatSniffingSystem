using RatSniffingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Contracts.Interfaces
{
    public interface IRatWeightService<TDto, TCreateDto> : IGenericService<TDto, TCreateDto, object>
        where TCreateDto : class
        where TDto : class
    {
        /// <summary>
        /// Belirli bir kilo araligina sahip hayvani getirir
        /// </summary>

        Task<List<TDto>> GetByWeightRangeAsync(decimal firstGram, decimal LastGram);
    }
}
