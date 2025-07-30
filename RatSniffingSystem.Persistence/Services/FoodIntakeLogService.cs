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
    public class FoodIntakeLogService : SessionLinkedServiceBase<FoodIntakeLog, FoodIntakeLogDto, CreateFoodIntakeLogDto, object>, IFoodIntakeLogService<FoodIntakeLog,FoodIntakeLogDto, CreateFoodIntakeLogDto>
    {
        public FoodIntakeLogService(AppDbContext context, IMapper mapper, ILogger<FoodIntakeLogService> logger) : base(context, mapper, logger)
        {
        }

        public async Task<List<FoodIntakeLogDto>> GetByCcRangeAsync(double first, double last)
        {
            var foodIntakeLogs = await _dbSet
                .Where(f => f.AmountInCc >= first && f.AmountInCc <= last)
                .ToListAsync();

            if (!foodIntakeLogs.Any())
            {
                _logger.LogWarning("No food intake logs found in the range {First} to {Last} cc.", first, last);
                return null;
            }
            return _mapper.Map<List<FoodIntakeLogDto>>(foodIntakeLogs);
        }
    }
}
