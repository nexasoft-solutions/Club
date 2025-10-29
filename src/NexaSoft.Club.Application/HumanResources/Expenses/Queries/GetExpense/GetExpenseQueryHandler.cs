using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.Expenses;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.Expenses.Queries.GetExpense;

public class GetExpenseQueryHandler(
    IGenericRepository<Expense> _repository
) : IQueryHandler<GetExpenseQuery, ExpenseResponse>
{
    public async Task<Result<ExpenseResponse>> Handle(GetExpenseQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new ExpenseSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<ExpenseResponse>(ExpenseErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<ExpenseResponse>(ExpenseErrores.ErrorConsulta);
        }
    }
}
