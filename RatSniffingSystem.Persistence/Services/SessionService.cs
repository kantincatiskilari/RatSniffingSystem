using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RatSniffingSystem.Application.DTOs;
using RatSniffingSystem.Application.Exceptions;
using RatSniffingSystem.Application.Interfaces;
using RatSniffingSystem.Contracts.Interfaces;
using RatSniffingSystem.Domain.Entity;
using RatSniffingSystem.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Persistence.Services
{
    public class SessionService : GenericServiceBase<Session, SessionDto, CreateSessionDto, UpdateSessionDto>, ISessionService<SessionDto, CreateSessionDto, UpdateSessionDto>
    {
        public SessionService(AppDbContext context, IMapper mapper, ILogger<SessionService> logger) : base(context, mapper, logger)
        {
        }

        // Ortak method
        private async Task<List<SessionDto>> GetSessionsAsync(Expression<Func<Session, bool>> predicate, string notFoundMessageFormat, params object[] logArgs)
        {
            var sessions = await _dbSet.Where(predicate).ToListAsync();

            if (!sessions.Any())
            {
                _logger.LogWarning(notFoundMessageFormat, logArgs);
                throw new NotFoundException(string.Format(notFoundMessageFormat, logArgs));
            }

            return _mapper.Map<List<SessionDto>>(sessions);
        }

        public async Task<double> GetAverageDurationAsync()
        {

            var allDurations = await _dbSet
             .Where(s => s.DurationMinutes > 0)
             .Select(s => s.DurationMinutes)
             .ToListAsync();

            if (!allDurations.Any())
            {
                _logger.LogWarning("No session durations found for average calculation.");
                throw new BusinessException("No valid session durations available.");
            }

            var average = allDurations.Average();
            _logger.LogInformation("Calculated average session duration: {Avg}", average);
            return average;
        }

        public Task<List<SessionDto>> GetByCageCodeAsync(string cageCode) =>
            GetSessionsAsync(s => s.CageCode == cageCode,
                             "No sessions found for cage code: {0}",
                             cageCode);

        public Task<List<SessionDto>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var endOfDay = endDate.Date.AddDays(1).AddTicks(-1);
            return GetSessionsAsync(
                s => s.SessionDate >= startDate.Date && s.SessionDate <= endOfDay,
                "No sessions found between {0} and {1}",
                startDate, endDate
            );
        }

        public Task<List<SessionDto>> GetByDurationRangeAsync(int minDurationMinutes, int maxDurationMinutes) =>
           GetSessionsAsync(s => s.DurationMinutes >= minDurationMinutes && s.DurationMinutes <= maxDurationMinutes,
                            "No sessions found with duration between {0} and {1} minutes",
                            minDurationMinutes, maxDurationMinutes);

        public Task<List<SessionDto>> GetByMaterialTypeAsync(string materialType) =>
           GetSessionsAsync(s => s.MaterialType == materialType,
                            "No sessions found with material type: {0}",
                            materialType);

        public Task<List<SessionDto>> GetByRatIdAsync(Guid ratId) =>
         GetSessionsAsync(s => s.RatId == ratId,
                          "No sessions found for RatId: {0}",
                          ratId);

        public Task<List<SessionDto>> GetByTrainerIdAsync(Guid trainerId) =>
            GetSessionsAsync(s => s.TrainerId == trainerId,
                             "No sessions found for TrainerId: {0}",
                             trainerId);
    }
}
