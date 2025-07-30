
using RatSniffingSystem.Domain.Contracts;
using RatSniffingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Contracts.Interfaces
{
    public interface ITrainerService<TEntity, TDto, TCreateDto, TUpdateDto> : IGenericService<TEntity, TDto, TCreateDto, TUpdateDto>
        where TEntity : class, IHasId
        where TCreateDto : class
        where TUpdateDto : class
        where TDto : class
    {
        /// <summary>
        /// Belirli bir emaile sahip egitmeni getirir
        /// </summary>

        Task<TDto?> GetByEmailAsync(string email);
    }
}
