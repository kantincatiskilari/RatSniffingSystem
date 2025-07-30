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
    public class InterventionService : SessionLinkedServiceBase<Intervention, InterventionDto, CreateInterventionDto, object>, IInterventionService<InterventionDto, CreateInterventionDto>
    {
        public InterventionService(AppDbContext context, IMapper mapper, ILogger logger) : base(context, mapper, logger)
        {
        }

        public async Task<List<InterventionDto>> GetBySubstanceAsync(string substance)
        {
            var interventions = await _dbSet
                .Where(i => i.Substance.Equals(substance, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
            if (!interventions.Any())
            {
                _logger.LogWarning("No interventions found for substance: {Substance}", substance);
                throw new NotFoundException($"No interventions found for substance: {substance}");
            }

            return _mapper.Map<List<InterventionDto>>(interventions);
        }    
    }
}
