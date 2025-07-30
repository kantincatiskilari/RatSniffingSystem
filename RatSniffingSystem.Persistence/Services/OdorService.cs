using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RatSniffingSystem.Application.DTOs;
using RatSniffingSystem.Application.Interfaces;
using RatSniffingSystem.Domain.Entities;
using RatSniffingSystem.Domain.Enums;
using RatSniffingSystem.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RatSniffingSystem.Application.Exceptions;
using RatSniffingSystem.Contracts.Interfaces;

namespace RatSniffingSystem.Persistence.Services
{
    public class OdorService : GenericServiceBase<Odor, OdorDto, CreateOdorDto, object>, IOdorService<OdorDto, CreateOdorDto>
    {
        public OdorService(AppDbContext context, IMapper mapper, ILogger logger) : base(context, mapper, logger)
        {
        }

        public async Task<List<OdorDto>> GetByCategoryAsync(OdorCategory category)
        {
            var odors = await _dbSet
                .Where(o => o.OdorCategory == category)
                .ToListAsync();

            if (!odors.Any())
            {
                _logger.LogWarning("No odors found for category {Category}.", category);
                throw new NotFoundException($"No odors found for category {category}.");
            }

            return _mapper.Map<List<OdorDto>>(odors);
        }

        public Task<OdorDto> GetByExternalCodeAsync(string externalCode)
        {
            throw new NotImplementedException();
        }

        public Task<OdorDto> GetByHazardousStatusAsync(bool isHazardous)
        {
            throw new NotImplementedException();
        }

        public Task<OdorDto> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
