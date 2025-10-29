using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.Expenses;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.Expenses.Queries.GetExpenses;

public class GetExpensesQueryHandler(
    IGenericRepository<Expense> _repository
) : IQueryHandler<GetExpensesQuery, Pagination<ExpenseResponse>>
{
    public async Task<Result<Pagination<ExpenseResponse>>> Handle(GetExpensesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new ExpenseSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<ExpenseResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<ExpenseResponse>(
                    query.SpecParams.PageIndex,
                    query.SpecParams.PageSize,
                    totalItems,
                    responses
              );

            return Result.Success(pagination);

        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<Pagination<ExpenseResponse>>(ExpenseErrores.ErrorConsulta);
        }
    }
}
