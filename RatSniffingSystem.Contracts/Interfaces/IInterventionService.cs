
using RatSniffingSystem.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Contracts.Interfaces
{
    public interface IInterventionService<TEntity, TDto, TCreateDto> : IGenericService<TEntity, TDto, TCreateDto, object> 
        where TEntity : class, IHasId
        where TCreateDto : class 
        where TDto : class
    {
        /// <summary>
        /// Verilen madde ismine gore kayitlari getirir
        /// </summary>
        Task<List<TDto>> GetBySubstanceAsync(string substance);
    }
}
