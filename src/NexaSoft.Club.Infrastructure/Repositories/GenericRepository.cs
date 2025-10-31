using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Infrastructure.Repositories;

public class GenericRepository<T>(ApplicationDbContext _dbContext) : IGenericRepository<T> where T : Entity
{
    public async Task<T?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var query = ApplyGlobalFilter(_dbContext.Set<T>());
        return await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
    {
        await _dbContext.Set<T>().AddRangeAsync(entities, cancellationToken);
    }

    public async Task<IReadOnlyList<T>> ListAsync(CancellationToken cancellationToken)
    {
        var query = ApplyGlobalFilter(_dbContext.Set<T>());
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<T>> ListAsync(
    Expression<Func<T, bool>> predicate,
    string[]? includes,
    CancellationToken cancellationToken)
    {
        IQueryable<T> query = ApplyGlobalFilter(_dbContext.Set<T>());

        // Aplicar includes si existen
        if (includes is not null)
        {
            foreach (var include in includes)
                query = query.Include(include);
        }

        query = query.Where(predicate);

        return await query.ToListAsync(cancellationToken);
    }


    public async Task<T?> GetEntityWithSpec(ISpecification<T> spec, CancellationToken cancellationToken)
    {
        var query = ApplySpecification(spec);
        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken)
    {
        var query = ApplySpecification(spec);
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<TResult?> GetEntityWithSpec<TResult>(ISpecification<T, TResult> spec, CancellationToken cancellationToken)
    {
        var query = ApplySpecification(spec);
        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<T, TResult> spec, CancellationToken cancellationToken)
    {
        var query = ApplySpecification(spec);
        return await query.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
    }

    public async Task UpdateAsync(T entity)
    {
        _dbContext.Set<T>().Attach(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        await Task.CompletedTask;
    }

    public async Task<bool> ExistsAsync(long id, CancellationToken cancellationToken)
    {
        var query = ApplyGlobalFilter(_dbContext.Set<T>());
        return await query.AnyAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        var query = ApplyGlobalFilter(_dbContext.Set<T>());
        return await query.AnyAsync(predicate, cancellationToken);
    }

    public async Task<int> CountAsync(ISpecification<T> spec, CancellationToken cancellationToken)
    {
        var query = ApplyGlobalFilter(_dbContext.Set<T>());
        query = spec.ApplyCriteria(query);
        return await query.CountAsync(cancellationToken);
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        var query = ApplyGlobalFilter(_dbContext.Set<T>());
        return await query.CountAsync(predicate, cancellationToken);
    }


    public async Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
    {
        foreach (var entity in entities)
        {
            _dbContext.Set<T>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
        await Task.CompletedTask;
    }

    // ✅ MÉTODO AUXILIAR QUE FALTA EN LA INTERFAZ PERO ES NECESARIO
    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        var query = ApplyGlobalFilter(_dbContext.Set<T>());
        return await query.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        var query = ApplyGlobalFilter(_dbContext.Set<T>());
        query = query.Where(predicate);
        return await query.ToListAsync(cancellationToken);
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        var query = ApplyGlobalFilter(_dbContext.Set<T>());
        return SpecificationEvaluator<T>.GetQuery(query, spec);
    }

    private IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> spec)
    {
        var query = ApplyGlobalFilter(_dbContext.Set<T>());
        return SpecificationEvaluator<T>.GetQuery<T, TResult>(query, spec);
    }

    private static IQueryable<T> ApplyGlobalFilter(IQueryable<T> query)
    {
        return query.Where(e => e.DeletedAt == null);
    }
}