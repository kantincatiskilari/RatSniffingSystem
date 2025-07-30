using RatSniffingSystem.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Contracts.Interfaces
{
    public interface IExperimentalNoteService<TEntity, TDto, TCreateDto> : IGenericService<TEntity, TDto, TCreateDto, object>
        where TEntity : class, IHasId
        where TCreateDto : class
        where TDto : class
    {
        /// <summary>
        /// Belirli bir baslik etiketine sahip deney notlarini getirir
        /// </summary>
        Task<TDto?> GetByTitleAsync(string title);
    }
}
