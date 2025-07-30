using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RatSniffingSystem.Application.Exceptions;
using RatSniffingSystem.Domain.Contracts;

using RatSniffingSystem.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RatSniffingSystem.Persistence.Services
{
    public abstract class SessionLinkedServiceBase<TEntity, TDto, TCreateDto, TUpdateDto>
        : GenericServiceBase<TEntity, TDto, TCreateDto, TUpdateDto>
        where TEntity : class, ISessionLinkedEntity, IHasId 
        where TDto : class
        where TCreateDto : class
        where TUpdateDto : class
    {
        protected SessionLinkedServiceBase(AppDbContext context, IMapper mapper, ILogger logger)
            : base(context, mapper, logger) { }

        /// <summary>Belirtilen oturuma bağlı tüm kayıtları döner (boş olabilir).</summary>
        public virtual async Task<IReadOnlyList<TDto>> GetBySessionIdAsync(Guid sessionId, CancellationToken ct = default)
        {
            var items = await _dbSet
                .AsNoTracking()
                .Where(e => e.SessionId == sessionId)
                .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);

            _logger.LogInformation("{Entity} by SessionId={SessionId} -> {Count} item(s).",
                typeof(TEntity).Name, sessionId, items.Count);

            // Koleksiyonlar için 200 + [] doğru semantik; 404 atmayalım.
            return items;
        }

        // (Opsiyonel) Sayfalı sürüm
        public virtual async Task<(IReadOnlyList<TDto> Items, int TotalCount)> GetPageBySessionIdAsync(
            Guid sessionId, int page, int pageSize, CancellationToken ct = default)
        {
            var query = _dbSet.AsNoTracking().Where(e => e.SessionId == sessionId);

            var total = await query.CountAsync(ct);

           
            var items = await query
                .OrderBy(e => e.Id) 
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);

            return (items, total);
        }
    }
}
