using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RatSniffingSystem.Application.DTOs;
using RatSniffingSystem.Application.Exceptions;
using RatSniffingSystem.Application.Interfaces;
using RatSniffingSystem.Contracts.Interfaces;
using RatSniffingSystem.Domain.Entities;
using RatSniffingSystem.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Persistence.Services
{
    public class InterventionService : SessionLinkedServiceBase<Intervention, InterventionDto, CreateInterventionDto, object>, IInterventionService<Intervention,InterventionDto, CreateInterventionDto>
    {
        public InterventionService(AppDbContext context, IMapper mapper, ILogger<InterventionService> logger) : base(context, mapper, logger)
        {
        }

        public async Task<List<InterventionDto>> GetBySubstanceAsync(string substance)
        {
            if (string.IsNullOrWhiteSpace(substance))
                throw new ArgumentException("Substance cannot be null or empty.");

            var interventions = await _dbSet
                .Where(i => i.Substance.ToLower() == substance.ToLower())
                .ToListAsync();

            if (!interventions.Any())
            {
                _logger.LogWarning("No interventions found for substance: {Substance}", substance);
                return null;
            }

            return _mapper.Map<List<InterventionDto>>(interventions);
        }

    }
}
