using System.Linq.Expressions;
using RatSniffingSystem.Domain.Contracts;
using RatSniffingSystem.Domain.Contracts;

namespace RatSniffingSystem.Contracts.Interfaces
{
    public interface IGenericService<TEntity, TDto, TCreateDto, TUpdateDto>
        where TEntity : class, IHasId
        where TDto : class
        where TCreateDto : class
        where TUpdateDto : class
    {
        // CREATE / UPDATE / DELETE
        Task<TDto> CreateAsync(TCreateDto dto, CancellationToken ct = default);
        Task<TDto> UpdateAsync(Guid id, TUpdateDto dto, CancellationToken ct = default);
        Task DeleteAsync(Guid id, CancellationToken ct = default);

        // READ (single / list)
        Task<TDto> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IReadOnlyList<TDto>> GetAllAsync(CancellationToken ct = default);

        // EXISTS / COUNT
        Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
        Task<int> CountAsync(CancellationToken ct = default);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default);

        // QUERY HELPERS (server-side predicate)
        Task<TDto?> FindFirstAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default);
        Task<IReadOnlyList<TDto>> WhereAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default);
    }
}
