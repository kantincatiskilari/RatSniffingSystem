using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RatSniffingSystem.Application.DTOs;
using RatSniffingSystem.Application.Exceptions;
using RatSniffingSystem.Application.Interfaces;
using RatSniffingSystem.Contracts.Interfaces;
using RatSniffingSystem.Domain.Entities;
using RatSniffingSystem.Domain.Entity;
using RatSniffingSystem.Domain.Enums;
using RatSniffingSystem.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Persistence.Services
{
    public class TrialService : SessionLinkedServiceBase<Trial, TrialDto, CreateTrialDto, object>, ITrialService<TrialDto, CreateTrialDto>
    {
        public TrialService(AppDbContext context, IMapper mapper, ILogger logger) : base(context, mapper, logger)
        {
        }

        // Ortak method
        private async Task<List<TrialDto>> GetTrialsAsync(Expression<Func<Trial, bool>> predicate, string notFoundMessageFormat, params object[] logArgs)
        {
            var trials = await _dbSet.Where(predicate).ToListAsync();

            if (!trials.Any())
            {
                _logger.LogWarning(notFoundMessageFormat, logArgs);
                throw new NotFoundException(string.Format(notFoundMessageFormat, logArgs));
            }

            return _mapper.Map<List<TrialDto>>(trials);
        }

        

        public Task<List<TrialDto>> GetByTargetOdorAsync(string targetOdor) => GetTrialsAsync(
            t => t.TargetOdor == targetOdor,
            "No trials found with odor: {0}",
            targetOdor
            );

        public Task<List<TrialDto>> GetByTrialNumberAsync(int trialNumber) => GetTrialsAsync(
            t => t.TrialNumber == trialNumber,
            "No trial found by trial number: {0}",
            trialNumber
            );

        public Task<List<TrialDto>> GetCorrectResponsesAsync(Guid sessionId) => GetTrialsAsync(
            t => t.IsCorrectPositive && t.IsCorrectNegative && t.SessionId == sessionId,
            "No trial found with these response standarts in this session {0}, {1}",
            "Correct Negative", "Correct Positive"
            );

        public Task<List<TrialDto>> GetIncorrectResponsesAsync(Guid sessionId)
       => GetTrialsAsync(
            t => t.IsFalsePositive && t.IsFalseNegative && t.SessionId == sessionId,
            "No trial found with these response standarts in this session {0}, {1}",
            "False Negative", "False Positive"
            );

        public async Task<double> GetAverageResponseTimeAsync(Guid sessionId)
        {
         
            var trials = await _dbSet
                .Where(t => t.SessionId == sessionId)
                .ToListAsync();

            if (!trials.Any())
            {
                _logger.LogWarning("No trials found for session ID: {SessionId}", sessionId);
                throw new NotFoundException($"No trials found for session ID: {sessionId}");
            }

            var averageSeconds = trials
                .Average(t => (t.FirstResponseTime - t.Session.SessionDate).TotalSeconds);

            _logger.LogInformation("Average response time for session {SessionId}: {AvgSeconds} seconds", sessionId, averageSeconds);

            return averageSeconds;
        }
    }
    
}
