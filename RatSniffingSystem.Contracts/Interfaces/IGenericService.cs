using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Contracts.Interfaces
{
    public interface IGenericService<TDto, in TCreateDto, in TUpdateDto>
        where TDto : class
        where TCreateDto : class
        where TUpdateDto : class
    {
        Task<List<TDto>> GetAllAsync();
        Task<TDto?> GetByIdAsync(Guid id);
        Task<TDto> CreateAsync(TCreateDto dto);
        Task<bool> UpdateAsync(TUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);

        /// <summary>
        /// Sarta gore var mi kontrolu yapar
        /// </summary>
        
        Task<bool> ExistsAsync(Guid id);

        /// <summary>
        /// Tum kayit sayisini doner
        /// </summary>
        
        Task<int> GetCountAsync();

        /// <summary>
        /// Filtrelenmiş kayıt sayısını döndürür.
        /// </summary>
        Task<int> CountAsync(Func<TDto, bool> predicate);

        /// <summary>
        /// Verilen predicate'e göre ilk eşleşen DTO'yu döndürür (dikkatli kullanılmalı).
        /// </summary>
        Task<TDto?> FindFirstAsync(Func<TDto, bool> predicate);

        /// <summary>
        /// Verilen predicate'e göre filtrelenmiş DTO listesi döndürür.
        /// </summary>
        Task<List<TDto>> WhereAsync(Func<TDto, bool> predicate);
    }
}
