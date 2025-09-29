using System.Linq.Expressions;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        protected BaseSpecification()
        {
            Includes = new List<Expression<Func<T, object>>>();
            IncludeStrings = new List<string>();
        }

        protected BaseSpecification(Expression<Func<T, bool>> criteria) : this()
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>>? Criteria { get; protected set; }

        public Expression<Func<T, object>>? OrderBy { get; private set; }

        public Expression<Func<T, object>>? OrderByDescending { get; private set; }

        public List<Expression<Func<T, object>>> Includes { get; }

        public List<string> IncludeStrings { get; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; private set; }

        public bool IsDistinct { get; private set; }

        public IQueryable<T> ApplyCriteria(IQueryable<T> query)
        {
            if (Criteria is not null)
                query = query.Where(Criteria);

            return query;
        }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }

        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }

        /// <summary>
        /// Aplica paginación solo si IsPagingEnabled es true.
        /// </summary>
        /// <param name="skip">Número de elementos a saltar</param>
        /// <param name="take">Número de elementos a tomar</param>
        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }

        protected void ApplyDistinct()
        {
            IsDistinct = true;
        }

        /// <summary>
        /// Agrega un nuevo criterio usando lógica AND sobre el criterio existente.
        /// </summary>
        protected void AddCriteria(Expression<Func<T, bool>> newCriteria)
        {
            if (Criteria is null)
                Criteria = newCriteria;
            else
                Criteria = Criteria.And(newCriteria);
        }

        /// <summary>
        /// Método genérico para filtrar por una lista de IDs (o cualquier tipo TKey).
        /// </summary>
        protected void AddIdsFilter<TKey>(Expression<Func<T, TKey>> idSelector, List<TKey> ids)
            where TKey : struct, IEquatable<TKey>
        {
            if (ids != null && ids.Any())
            {
                AddCriteria(BuildContainsExpression(idSelector, ids));
            }
        }

        private Expression<Func<T, bool>> BuildContainsExpression<TKey>(Expression<Func<T, TKey>> selector, List<TKey> ids)
        {
            var parameter = selector.Parameters.Single();
            var body = Expression.Call(
                typeof(Enumerable),
                nameof(Enumerable.Contains),
                new Type[] { typeof(TKey) },
                Expression.Constant(ids),
                selector.Body
            );
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }

    public class BaseSpecification<T, TResult> : BaseSpecification<T>, ISpecification<T, TResult>
    {
        protected BaseSpecification() : base() { }

        protected BaseSpecification(Expression<Func<T, bool>> criteria) : base(criteria) { }

        public Expression<Func<T, TResult>>? Select { get; private set; }

        protected void AddSelect(Expression<Func<T, TResult>> selectExpression)
        {
            Select = selectExpression;
        }
    }
}
