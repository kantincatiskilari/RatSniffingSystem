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
    public class TrialOdorService : GenericServiceBase<TrialOdor, TrialOdorDto, CreateTrialOdorDto, object>, ITrialOdorService<TrialOdorDto, CreateTrialOdorDto>
    {
        public TrialOdorService(AppDbContext context, IMapper mapper, ILogger logger) : base(context, mapper, logger)
        {
        }

        // Ortak method
        private async Task<List<TrialOdorDto>> GetTrialOdorsAsync(Expression<Func<TrialOdor, bool>> predicate, string notFoundMessageFormat, params object[] logArgs)
        {
            var trialOdors = await _dbSet.Where(predicate).ToListAsync();

            if (!trialOdors.Any())
            {
                _logger.LogWarning(notFoundMessageFormat, logArgs);
                throw new NotFoundException(string.Format(notFoundMessageFormat, logArgs));
            }

            return _mapper.Map<List<TrialOdorDto>>(trialOdors);
        }

        public Task<List<TrialOdorDto>> GetByOdorIdAsync(Guid odorId) => GetTrialOdorsAsync(
            to => to.OdorId == odorId,
            "Trial Odors not found with Odor Id: {0}",
            odorId
            );
        

        public Task<List<TrialOdorDto>> GetByOdorTypeAsync(OdorType odorType)
        => GetTrialOdorsAsync(
            to => to.OdorType == odorType,
            "Trial Odors not found with Odor Type: {0}",
            odorType
            );

        public Task<List<TrialOdorDto>> GetByPositionIndexAsync(int positionIndex)
        => GetTrialOdorsAsync(
            to => to.PositionIndex == positionIndex,
            "Trial Odors not found with Position Index: {0}",
            positionIndex
            );

        public Task<List<TrialOdorDto>> GetByTrialIdAsync(Guid trialId)
        => GetTrialOdorsAsync(
            to => to.TrialId == trialId,
            "Trial Odors not found with Trial Id: {0}",
            trialId
            );

        public Task<List<TrialOdorDto>> GetFalsePositivesAsync()
        => GetTrialOdorsAsync(
            to => to.IsFalsePositive,
            "Trial Odors not found with criteria: {0}",
            "False Positive"
            );

        public Task<List<TrialOdorDto>> GetFalsePositivesByTrialIdAsync(Guid trialId)
           => GetTrialOdorsAsync(
            to => to.IsFalsePositive && to.TrialId == trialId,
            "Trial Odors not found with criteria: {0} with Trial Id {1}",
            "False Positive",trialId
            );
    }
}
