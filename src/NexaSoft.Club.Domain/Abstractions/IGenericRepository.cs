using System.Linq.Expressions;

namespace NexaSoft.Club.Domain.Abstractions;

public interface IGenericRepository<T> where T : Entity
{
    Task<T?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);
    Task<IReadOnlyList<T>> ListAsync(CancellationToken cancellationToken);
    Task<T?> GetEntityWithSpec(ISpecification<T> spec, CancellationToken cancellationToken);
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken);

    Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> predicate, string[]? includes, CancellationToken cancellationToken);

    Task<TResult?> GetEntityWithSpec<TResult>(ISpecification<T, TResult> spec, CancellationToken cancellationToken);
    Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<T, TResult> spec, CancellationToken cancellationToken);
    Task AddAsync(T entity, CancellationToken cancellationToken);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<bool> ExistsAsync(long id, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    Task<int> CountAsync(ISpecification<T> spec, CancellationToken cancellationToken);
    Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);

    Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);

    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
     // ✅ NUEVO MÉTODO PARA ListAsync CON EXPRESIÓN LAMBDA
    Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);

}