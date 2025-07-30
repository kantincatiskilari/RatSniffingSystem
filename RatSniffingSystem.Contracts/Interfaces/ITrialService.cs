
using RatSniffingSystem.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RatSniffingSystem.Contracts.Interfaces
{

    public interface ITrialService<TEntity, TDto, TCreateDto> : IGenericService<TEntity, TDto, TCreateDto, object>
        where TEntity : class, IHasId
        where TCreateDto : class
        where TDto : class
    {
       

        /// <summary>
        /// Belirli bir sıra numarasına (trial number) sahip denemeleri getirir.
        /// </summary>
        /// <param name="trialNumber">Denemenin sıra numarası</param>
        /// <returns>Uygun Trial DTO listesi</returns>
        Task<List<TDto>> GetByTrialNumberAsync(int trialNumber);

        /// <summary>
        /// Belirli bir hedef kokuya (target odor) sahip denemeleri getirir.
        /// </summary>
        /// <param name="targetOdor">Hedef koku değeri</param>
        /// <returns>Uygun Trial DTO listesi</returns>
        Task<List<TDto>> GetByTargetOdorAsync(string targetOdor);

        /// <summary>
        /// Belirli bir oturumda doğru yanıt (TP veya TN) verilmiş tüm denemeleri getirir.
        /// </summary>
        /// <param name="sessionId">Session ID</param>
        /// <returns>Doğru yanıtlara sahip Trial DTO listesi</returns>
        Task<List<TDto>> GetCorrectResponsesAsync(Guid sessionId);

        /// <summary>
        /// Belirli bir oturumda yanlış yanıt (FP veya FN) verilmiş tüm denemeleri getirir.
        /// </summary>
        /// <param name="sessionId">Session ID</param>
        /// <returns>Yanlış yanıtlara sahip Trial DTO listesi</returns>
        Task<List<TDto>> GetIncorrectResponsesAsync(Guid sessionId);

        /// <summary>
        /// Belirli bir oturumdaki denemelere ait ortalama ilk yanıt süresini (FirstResponseTime) hesaplar.
        /// </summary>
        /// <param name="sessionId">Session ID</param>
        /// <returns>Ortalama yanıt süresi (saniye cinsinden)</returns>
        Task<double> GetAverageResponseTimeAsync(Guid sessionId);
    }
}
