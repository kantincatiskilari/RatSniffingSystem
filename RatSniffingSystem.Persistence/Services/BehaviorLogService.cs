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
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Persistence.Services
{
    public class BehaviorLogService : SessionLinkedServiceBase<BehaviorLog, BehaviorLogDto, CreateBehaviorLogDto, object> , IBehaviorLogService<BehaviorLogDto, CreateBehaviorLogDto>
    {
        public BehaviorLogService(AppDbContext context, IMapper mapper, ILogger<BehaviorLog> logger) : base(context, mapper, logger)
        {
        }

        public async Task<List<BehaviorLogDto>> GetByBehaviorTypeAsync(BehaviorType type)
        {
            var logs = await _dbSet
                .Where(log => log.BehaviorType == type)
                .ToListAsync();

            if (!logs.Any())
            {
                _logger.LogWarning("Behavior with type {type} not found.", type);
                throw new NotFoundException($"Behavior with type: {type} not found.");
            }
            return _mapper.Map<List<BehaviorLogDto>>(logs);
        }
    }
}
