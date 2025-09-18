using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Infrastructure.Repositories;

public class GenericRepository<T>(ApplicationDbContext _dbContext) : IGenericRepository<T> where T : Entity
{
    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
    }

    public async Task<int> CountAsync(ISpecification<T> spec, CancellationToken cancellationToken)
    {
        var query = _dbContext.Set<T>().AsQueryable();
        query = GenericRepository<T>.ApplyGlobalFilter(query); // Aplicar filtro global
        query = spec.ApplyCriteria(query);
        return await query.CountAsync(cancellationToken);
    }

    public async Task DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        await Task.CompletedTask;
    }

    public async Task<bool> ExistsAsync(long id, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<T>()
            .Where(e => e.FechaEliminacion == null) // Aplicar filtro global
            .AnyAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<T?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<T>()
            .Where(e => e.FechaEliminacion == null) // Aplicar filtro global
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<T?> GetEntityWithSpec(ISpecification<T> spec, CancellationToken cancellationToken)
    {
        var query = ApplySpecification(spec);
        query = ApplyGlobalFilter(query); // Aplicar filtro global
        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TResult?> GetEntityWithSpec<TResult>(ISpecification<T, TResult> spec, CancellationToken cancellationToken)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<T>> ListAsync(CancellationToken cancellationToken)
    {
        var query = ApplyGlobalFilter(_dbContext.Set<T>()); // ✅ Centraliza el filtro
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken)
    {
        return await ApplySpecification(spec).ToListAsync(cancellationToken); 
    }

    public async Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<T, TResult> spec, CancellationToken cancellationToken)
    {
        return await ApplySpecification(spec).ToListAsync(cancellationToken); 
    }

    public async Task UpdateAsync(T entity)
    {
        _dbContext.Set<T>().Attach(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;
        await Task.CompletedTask;
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        var query = _dbContext.Set<T>().AsQueryable();
        query = ApplyGlobalFilter(query); // ✅ Filtro global para T
        return SpecificationEvaluator<T>.GetQuery(query, spec);
    }

    private IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> spec)
    {
        var query = _dbContext.Set<T>().AsQueryable();
        query = ApplyGlobalFilter(query); // ✅ Aplica filtro global ANTES de proyectar
        return SpecificationEvaluator<T>.GetQuery<T, TResult>(query, spec);
    }

    // Método para aplicar el filtro global
    private static IQueryable<T> ApplyGlobalFilter(IQueryable<T> query)
    {
        return query.Where(e => e.FechaEliminacion == null);
    }

    public Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return _dbContext.Set<T>().AnyAsync(predicate, cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
    {
        await _dbContext.Set<T>().AddRangeAsync(entities, cancellationToken);
    }
}
