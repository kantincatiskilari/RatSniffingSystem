using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Application.Interfaces
{
    public interface IGenericService<TDto, in TCreateDto, in TUpdateDto>
        where TDto : class
        where TCreateDto : class
        where TUpdateDto : class
    {
        Task<List<TDto>> GetAllAsync();
        Task<TDto> GetByIdAsync(Guid id);
        Task<TDto> CreateAsync(TCreateDto dto);
        Task<bool> UpdateAsync(TUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
