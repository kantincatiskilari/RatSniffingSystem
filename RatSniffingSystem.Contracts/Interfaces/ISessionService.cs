using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Contracts.Interfaces
{
    public interface ISessionService<TDto, TCreateDto, TUpdateDto> : IGenericService<TDto, TCreateDto, TUpdateDto>
        where TUpdateDto : class
        where TCreateDto : class
        where TDto : class
    {
        /// <summary>
        /// Belirli bir RatId'ye sahip oturumları getirir.
        /// </summary>

        Task<List<TDto>> GetByRatIdAsync(Guid ratId);

        /// <summary>
        /// Belirli bir TrainerId'ye sahip oturumları getirir.
        /// </summary>

        Task<List<TDto>> GetByTrainerIdAsync(Guid trainerId);

        /// <summary>
        /// Belirli bir kafes koduna sahip oturumları getirir.
        /// </summary>

        Task<List<TDto>> GetByCageCodeAsync(string cageCode);

        /// <summary>
        /// Belirli aralikta sureye sahip oturumları getirir.
        /// </summary>

        Task<List<TDto>> GetByDurationRangeAsync(int minDurationMinutes, int maxDurationMinutes);

        /// <summary>
        /// Belirtilen tarih aralığındaki tüm seansları getirir.
        /// </summary>
        Task<List<TDto>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Belirtilen materyal tipindeki tüm seansları getirir.
        /// </summary>
        
        Task<List<TDto>> GetByMaterialTypeAsync(string materialType);


        /// <summary>
        /// Tüm seanslarin ortalama sure hesabini getirir.
        /// </summary>

        Task<double> GetAverageDurationAsync();
    }
}
