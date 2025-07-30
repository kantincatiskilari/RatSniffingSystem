using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RatSniffingSystem.Application.Exceptions;
using RatSniffingSystem.Domain.Common;
using RatSniffingSystem.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Persistence.Services
{
    public abstract class SessionLinkedServiceBase<TEntity, TDto, TCreateDto, TUpdateDto>
     : GenericServiceBase<TEntity, TDto, TCreateDto, TUpdateDto>
     where TEntity : class, ISessionLinkedEntity
     where TDto : class
     where TCreateDto : class
     where TUpdateDto : class
    {
        protected SessionLinkedServiceBase(AppDbContext context, IMapper mapper, ILogger logger)
            : base(context, mapper, logger)
        {
        }

        public async Task<List<TDto>> GetBySessionIdAsync(Guid sessionId)
        {
            var list = await _dbSet
                .Where(e => e.SessionId == sessionId)
                .ToListAsync();

            if (list == null || !list.Any())
            {
                _logger.LogWarning("No entities found for Session ID: {SessionId}", sessionId);
                throw new NotFoundException($"No entities found for Session ID: {sessionId}");
            }

            return _mapper.Map<List<TDto>>(list);
        }
    }
}
