using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Contracts.Interfaces
{
    public interface IFoodIntakeLogService<TDto, TCreateDto> : IGenericService<TDto, TCreateDto, object> 
        where TCreateDto : class where TDto : class
    {
        /// <summary>
        /// Belirli bir cc deger araligina sahip kayitlari getirir
        /// </summary>
        Task<List<TDto>> GetByCcRangeAsync(double first, double last);
    }
}
