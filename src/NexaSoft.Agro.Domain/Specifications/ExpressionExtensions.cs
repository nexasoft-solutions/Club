using System.Linq.Expressions;

namespace NexaSoft.Agro.Domain.Specifications
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(
            this Expression<Func<T, bool>> left,
            Expression<Func<T, bool>> right)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceParameterVisitor(left.Parameters[0], parameter);
            var leftBody = leftVisitor.Visit(left.Body)!;

            var rightVisitor = new ReplaceParameterVisitor(right.Parameters[0], parameter);
            var rightBody = rightVisitor.Visit(right.Body)!;

            var andAlso = Expression.AndAlso(leftBody, rightBody);
            return Expression.Lambda<Func<T, bool>>(andAlso, parameter);
        }

        private class ReplaceParameterVisitor : ExpressionVisitor
        {
            private readonly ParameterExpression _oldParam;
            private readonly ParameterExpression _newParam;

            public ReplaceParameterVisitor(ParameterExpression oldParam, ParameterExpression newParam)
            {
                _oldParam = oldParam;
                _newParam = newParam;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return node == _oldParam ? _newParam : base.VisitParameter(node);
            }
        }
    }
}
