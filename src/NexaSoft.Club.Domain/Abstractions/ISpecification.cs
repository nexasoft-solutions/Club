namespace NexaSoft.Club.Domain.Abstractions;

using System.Linq.Expressions;

public interface ISpecification<T>
{
    // Filtro de criterio
    Expression<Func<T, bool>>? Criteria { get; }

    // Ordenamiento
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }

    // Includes para relaciones
    List<Expression<Func<T, object>>> Includes { get; }
    List<string> IncludeStrings { get; }

    // Distinción de resultados
    bool IsDistinct { get; }

    // Paginación
    int Take { get; }
    int Skip { get; }
    bool IsPagingEnabled { get; }

    // Aplicación del criterio al IQueryable
    IQueryable<T> ApplyCriteria(IQueryable<T> query);
}

public interface ISpecification<T, TResult> : ISpecification<T>
{
    Expression<Func<T, TResult>>? Select { get; }
}
