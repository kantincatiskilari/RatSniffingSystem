using RatSniffingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Contracts.Interfaces
{
    public interface IRatService<TDto, TCreateDto, TUpdateDto> : IGenericService<TDto, TCreateDto, TUpdateDto>
        where TCreateDto : class
        where TUpdateDto : class
        where TDto : class
    {
        /// <summary>
        /// Belirli bir proje etiketine sahip sicanlari getirir
        /// </summary>
        
        Task<List<TDto>> GetByProjectTagAsync(string projectTag);

        /// <summary>
        /// Belirli bir proje etiketine sahip sicanlari getirir
        /// </summary>

        Task<List<TDto>> GetByBirthDateRangeAsync(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Belirli bir koda sahip sicanlari getirir
        /// </summary>
        
        Task<TDto?> FindByCodeAsync(string code);

        /// <summary>
        /// Cinsiyete gore sicanlari getirir
        /// </summary>

        Task<List<TDto>> GetByGenderAsync(Gender gender);

        /// <summary>
        /// Aktiflik durumuna gore sicanlari getirir.
        /// </summary>
        
        Task<List<TDto>> GetByStatusAsync(bool isActive);
    }
}
