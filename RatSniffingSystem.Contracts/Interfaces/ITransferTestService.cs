
using RatSniffingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Contracts.Interfaces
{
    public interface ITransferTestService<TDto, TCreateDto> : IGenericService<TDto, TCreateDto, object>
        where TCreateDto : class
        where TDto : class
    {
        /// <summary>
        /// Basari durumuna gore kayitlari getirir
        /// </summary>

        Task<TDto?> GetBySuccessStatusAsync(bool status);

        /// <summary>
        /// %80 basariya ulasana kadar gecen deney oturum sayisini getirir
        /// </summary>

        Task<int> GetSessionToSuccessCountAsync(int minCount, int maxCount);
    }
}
