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
    public class RatWeightService : GenericServiceBase<RatWeight, RatWeightDto, CreateRatWeightDto, object>, IRatWeightService<RatWeightDto, CreateRatWeightDto>
    {
        public RatWeightService(AppDbContext context, IMapper mapper, ILogger logger) : base(context, mapper, logger)
        {
        }

        public async Task<List<RatWeightDto>> GetByWeightRangeAsync(decimal firstGram, decimal LastGram)
        {
            var weights = await _dbSet
                .Where(w => w.WeightInGrams >= firstGram && w.WeightInGrams <= LastGram)
                .ToListAsync();

            if (!weights.Any())
            {
                _logger.LogWarning("No rat weights found in the range {FirstGram} to {LastGram}.", firstGram, LastGram);
                throw new NotFoundException($"No rat weights found in the range {firstGram} to {LastGram}.");
            }

            return _mapper.Map<List<RatWeightDto>>(weights);
        }
    }
}
