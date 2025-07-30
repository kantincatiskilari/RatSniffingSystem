
using RatSniffingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RatSniffingSystem.Contracts.Interfaces
{

    public interface ITrialOdorService<TDto, TCreateDto> : IGenericService<TDto, TCreateDto, object>
        where TCreateDto : class
        where TDto : class
    {
        /// <summary>
        /// Belirli bir denemeye (trial) ait tüm kokuları getirir.
        /// </summary>
        /// <param name="trialId">Trial ID</param>
        /// <returns>TrialOdor DTO listesi</returns>
        Task<List<TDto>> GetByTrialIdAsync(Guid trialId);

        /// <summary>
        /// Belirli bir pozisyon indeksine sahip kokuları getirir.
        /// </summary>
        /// <param name="positionIndex">Koku pozisyonu (0–n)</param>
        /// <returns>TrialOdor DTO listesi</returns>
        Task<List<TDto>> GetByPositionIndexAsync(int positionIndex);

        /// <summary>
        /// Belirli bir odor ID’sine sahip tüm TrialOdor kayıtlarını getirir.
        /// </summary>
        /// <param name="odorId">Odor (koku) ID</param>
        /// <returns>TrialOdor DTO listesi</returns>
        Task<List<TDto>> GetByOdorIdAsync(Guid odorId);

        /// <summary>
        /// Belirli bir koku tipine (OdorType) sahip pozisyonları getirir.
        /// </summary>
        /// <param name="odorType">Koku tipi (örneğin Target, Distractor)</param>
        /// <returns>TrialOdor DTO listesi</returns>
        Task<List<TDto>> GetByOdorTypeAsync(OdorType odorType);

        /// <summary>
        /// False positive (yanlış pozitif) olarak işaretlenmiş tüm TrialOdor kayıtlarını getirir.
        /// </summary>
        /// <returns>False positive olarak işaretlenmiş TrialOdor DTO listesi</returns>
        Task<List<TDto>> GetFalsePositivesAsync();

        /// <summary>
        /// Belirli bir deneme içerisindeki false positive kokuları getirir.
        /// </summary>
        /// <param name="trialId">Trial ID</param>
        /// <returns>TrialOdor DTO listesi</returns>
        Task<List<TDto>> GetFalsePositivesByTrialIdAsync(Guid trialId);
    }
}
