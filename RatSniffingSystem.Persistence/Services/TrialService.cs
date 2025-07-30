using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RatSniffingSystem.Application.DTOs;
using RatSniffingSystem.Application.Exceptions;
using RatSniffingSystem.Application.Interfaces;
using RatSniffingSystem.Contracts.Interfaces;
using RatSniffingSystem.Domain.Entities;
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
    public class TrialService : SessionLinkedServiceBase<Trial, TrialDto, CreateTrialDto, object>, ITrialService<Trial,TrialDto, CreateTrialDto>
    {
        public TrialService(AppDbContext context, IMapper mapper, ILogger<TrialService> logger) : base(context, mapper, logger)
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

        public override async Task<TrialDto> CreateAsync(CreateTrialDto dto, CancellationToken ct = default)
        {
            if (dto == null)
                throw new ValidationException("Create DTO cannot be null.");

            // 🔹 1. Session var mı kontrol et
            var session = await _context.Sessions.FindAsync(new object[] { dto.SessionId }, ct);
            if (session == null)
                throw new NotFoundException($"Session {dto.SessionId} bulunamadı.");

            // 🔹 2. Trial oluştur
            var trial = _mapper.Map<Trial>(dto);
            trial.SessionId = session.Id;

            // 🔹 3. Session'ı attach et
            _context.Attach(session);

            // 🔹 4. Kayıt işlemi
            await _context.Trials.AddAsync(trial, ct);
            await _context.SaveChangesAsync(ct);

            _logger.LogInformation("Trial created successfully with SessionId={SessionId}", dto.SessionId);
            return _mapper.Map<TrialDto>(trial);
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
