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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Persistence.Services
{
    public class RatService : GenericServiceBase<Rat, RatDto, CreateRatDto, UpdateRatDto>, IRatService<Rat, RatDto,CreateRatDto, UpdateRatDto>
    {
        public RatService(AppDbContext context, IMapper mapper, ILogger<RatService> logger) : base(context, mapper, logger )
        {
        }

        // Ortak method
        private async Task<List<RatDto>> GetRatsAsync(Expression<Func<Rat, bool>> predicate, string notFoundMessageFormat, params object[] logArgs)
        {
            var rats = await _dbSet.Where(predicate).ToListAsync();

            if (!rats.Any())
            {
                _logger.LogWarning(notFoundMessageFormat, logArgs);
                throw new NotFoundException(string.Format(notFoundMessageFormat, logArgs));
            }

            return _mapper.Map<List<RatDto>>(rats);
        }

        public async Task<RatDto?> FindByCodeAsync(string code)
        {
            var entity = await _dbSet.Where(r => r.Code == code)
                .FirstOrDefaultAsync();
            return entity is null ? null : _mapper.Map<RatDto>(entity);
        }

        public async Task<List<RatDto>> GetByBirthDateRangeAsync(DateTime startDate, DateTime endDate) => await GetRatsAsync(
            r => r.BirthDate >= startDate && r.BirthDate <= endDate,
            "No rats found in the birth date range: {0} - {1}",
            startDate.ToShortDateString(), endDate.ToShortDateString());

        public async Task<List<RatDto>> GetByGenderAsync(Gender gender) => await GetRatsAsync(
            r => r.Gender == gender,
            "No rats found with gender: {0}",
            gender.ToString());

        public async Task<List<RatDto>> GetByProjectTagAsync(string projectTag)
        => await GetRatsAsync(
            r => r.ProjectTag.ToLower().Trim() == projectTag.ToLower().Trim(),
            "No rats found with project tag: {0}",
            projectTag.ToLower());

        public async Task<List<RatDto>> GetByStatusAsync(bool isActive)
         => await GetRatsAsync(
            r => r.IsActive == isActive,
            "No rats found with status: {0}",
            isActive);
    }
}
