using System.Linq.Expressions;

namespace NexaSoft.Agro.Domain.Abstractions;

public interface IGenericRepository<T> where T : Entity
{
    Task<T?> GetByIdAsync(Guid id,CancellationToken cancellationToken);
    Task<IReadOnlyList<T>> ListAsync(CancellationToken cancellationToken);
    Task<T?> GetEntityWithSpec(ISpecification<T> spec,CancellationToken cancellationToken);
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec,CancellationToken cancellationToken);
    Task<TResult?> GetEntityWithSpec<TResult>(ISpecification<T, TResult> spec, CancellationToken cancellationToken);
    Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<T, TResult> spec, CancellationToken cancellationToken);
    Task AddAsync(T entity,CancellationToken cancellationToken);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    Task<int> CountAsync(ISpecification<T> spec,CancellationToken cancellationToken);
}
