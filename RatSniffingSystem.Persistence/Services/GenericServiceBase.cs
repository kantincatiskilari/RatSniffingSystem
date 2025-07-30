using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RatSniffingSystem.Application.Exceptions;
using RatSniffingSystem.Application.Interfaces;
using RatSniffingSystem.Contracts.Interfaces;
using RatSniffingSystem.Persistence.Context;

namespace RatSniffingSystem.Persistence.Services
{
    public abstract class GenericServiceBase<TEntity, TDto, TCreateDto, TUpdateDto> : IGenericService<TDto, TCreateDto, TUpdateDto>
        where TEntity : class
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

        public virtual async Task<int> CountAsync(Func<TDto, bool> predicate)
        {
            var all = await GetAllAsync();
            return all.Count(predicate);
        }

        public virtual async Task<TDto> CreateAsync(TCreateDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("CreateAsync called with null DTO.");
                throw new ValidationException("Create DTO cannot be null.");
            }

            var entity = _mapper.Map<TEntity>(dto);
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            _logger.LogInformation("New {Entity} created successfully.", typeof(TEntity).Name);
            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity is null)
            {
                _logger.LogWarning("{Entity} not found for deletion. ID: {Id}", typeof(TEntity).Name, id);
                throw new NotFoundException($"Entity with ID {id} not found for deletion.");
            }

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            _logger.LogInformation("{Entity} with ID {Id} deleted successfully.", typeof(TEntity).Name, id);
            return true;
        }

        public virtual async Task<bool> ExistsAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            return entity != null;
        }

        public virtual async Task<TDto?> FindFirstAsync(Func<TDto, bool> predicate)
        {
            var all = await GetAllAsync();
            return all.FirstOrDefault(predicate);
        }

        public virtual async Task<List<TDto>> GetAllAsync()
        {
            var entities = await _dbSet.ToListAsync();
            _logger.LogInformation("Retrieved all {Entity} entities. Count: {Count}", typeof(TEntity).Name, entities.Count);
            return _mapper.Map<List<TDto>>(entities);
        }

        public virtual async Task<TDto> GetByIdAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity is null)
            {
                _logger.LogWarning("{Entity} with ID {Id} not found.", typeof(TEntity).Name, id);
                throw new NotFoundException($"Entity with ID {id} not found.");
            }

            _logger.LogInformation("{Entity} with ID {Id} retrieved successfully.", typeof(TEntity).Name, id);
            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task<int> GetCountAsync()
        {
            var count = await _dbSet.CountAsync();
            _logger.LogInformation("Total count for {Entity}: {Count}", typeof(TEntity).Name, count);
            return count;
        }

        public virtual async Task<bool> UpdateAsync(TUpdateDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("UpdateAsync called with null DTO.");
                throw new ValidationException("Update DTO cannot be null.");
            }

            var idProp = typeof(TUpdateDto).GetProperty("Id");
            if (idProp == null)
                throw new ValidationException("Update DTO must have an 'Id' property.");

            var idValue = idProp.GetValue(dto);
            if (idValue == null || !(idValue is Guid id))
                throw new ValidationException("Invalid or missing ID in Update DTO.");

            var existing = await _dbSet.FindAsync(id);
            if (existing == null)
            {
                _logger.LogWarning("{Entity} with ID {Id} not found for update.", typeof(TEntity).Name, id);
                throw new NotFoundException($"Entity with ID {id} not found for update.");
            }

            _mapper.Map(dto, existing);
            _dbSet.Update(existing);
            await _context.SaveChangesAsync();
            _logger.LogInformation("{Entity} with ID {Id} updated successfully.", typeof(TEntity).Name, id);
            return true;
        }

        public virtual async Task<List<TDto>> WhereAsync(Func<TDto, bool> predicate)
        {
            var all = await GetAllAsync();
            return all.Where(predicate).ToList();
        }
    }
}
