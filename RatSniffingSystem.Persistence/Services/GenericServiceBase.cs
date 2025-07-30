using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RatSniffingSystem.Application.Exceptions;
using RatSniffingSystem.Application.Interfaces;
using RatSniffingSystem.Contracts.Interfaces;
using RatSniffingSystem.Domain.Contracts;
using RatSniffingSystem.Persistence.Context;

namespace RatSniffingSystem.Persistence.Services
{
    public abstract class GenericServiceBase<TEntity, TDto, TCreateDto, TUpdateDto> : IGenericService<TEntity, TDto, TCreateDto, TUpdateDto>
        where TEntity : class, IHasId
        where TDto : class
        where TCreateDto : class
        where TUpdateDto : class
    {
        protected readonly AppDbContext _context;
        protected readonly IMapper _mapper;
        protected readonly DbSet<TEntity> _dbSet;
        protected readonly ILogger _logger;

        protected GenericServiceBase(AppDbContext context, IMapper mapper, ILogger logger)
        {
            _context = context;
            _mapper = mapper;
            _dbSet = _context.Set<TEntity>();
            _logger = logger;
        }

        protected virtual IQueryable<TEntity> Query => _dbSet.AsNoTracking();


        public virtual async Task<int> CountAsync(CancellationToken ct = default)
            => await Query.CountAsync(ct);

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default)
            => await Query.Where(predicate).CountAsync(ct);

        public virtual async Task<TDto> CreateAsync(TCreateDto dto, CancellationToken ct = default)
        {
            if (dto == null)
            {
                _logger.LogWarning("CreateAsync called with null DTO.");
                throw new ValidationException("Create DTO cannot be null.");
            }

            var entity = _mapper.Map<TEntity>(dto);
            await _dbSet.AddAsync(entity, ct);
            await _context.SaveChangesAsync(ct);

            _logger.LogInformation("New {Entity} created. Id={Id}", typeof(TEntity).Name, entity.Id);
            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var entity = await _dbSet.FindAsync(new object[] { id }, ct);
            if (entity is null)
            {
                _logger.LogWarning("{Entity} not found for deletion. ID: {Id}", typeof(TEntity).Name, id);
                throw new NotFoundException($"Entity with ID {id} not found for deletion.");
            }

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(ct);
            _logger.LogInformation("{Entity} with ID {Id} deleted.", typeof(TEntity).Name, id);
        }

        public virtual async Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
            => await Query.AnyAsync(e => e.Id == id, ct);

        public virtual async Task<TDto?> FindFirstAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default)
            => await Query.Where(predicate)
                          .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                          .FirstOrDefaultAsync(ct);

        public virtual async Task<IReadOnlyList<TDto>> GetAllAsync(CancellationToken ct = default)
            => await Query.ProjectTo<TDto>(_mapper.ConfigurationProvider).ToListAsync(ct);

        public virtual async Task<TDto> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var dto = await Query.Where(e => e.Id == id)
                                 .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                                 .SingleOrDefaultAsync(ct);
            if (dto is null)
            {
                _logger.LogWarning("{Entity} with ID {Id} not found.", typeof(TEntity).Name, id);
                throw new NotFoundException($"Entity with ID {id} not found.");
            }
            _logger.LogInformation("{Entity} with ID {Id} retrieved.", typeof(TEntity).Name, id);
            return dto;
        }

        public virtual async Task<TDto> UpdateAsync(Guid id, TUpdateDto dto, CancellationToken ct = default)
        {
            if (dto == null)
            {
                _logger.LogWarning("UpdateAsync called with null DTO.");
                throw new ValidationException("Update DTO cannot be null.");
            }

           
            var existing = await _dbSet.FirstOrDefaultAsync(e => e.Id == id, ct);
            if (existing is null)
            {
                _logger.LogWarning("{Entity} with ID {Id} not found for update.", typeof(TEntity).Name, id);
                throw new NotFoundException($"Entity with ID {id} not found for update.");
            }

            _mapper.Map(dto, existing);            
            await _context.SaveChangesAsync(ct);

            _logger.LogInformation("{Entity} with ID {Id} updated.", typeof(TEntity).Name, id);
            var result = _mapper.Map<TDto>(existing);
            return result;
        }

        public virtual async Task<IReadOnlyList<TDto>> WhereAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default)
            => await Query.Where(predicate)
                          .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                          .ToListAsync(ct);

        
    }
}
