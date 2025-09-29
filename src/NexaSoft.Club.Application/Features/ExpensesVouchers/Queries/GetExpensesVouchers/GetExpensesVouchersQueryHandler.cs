using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Features.ExpensesVouchers;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Features.ExpensesVouchers.Queries.GetExpensesVouchers;

public class GetExpensesVouchersQueryHandler(
    IGenericRepository<ExpenseVoucher> _repository
) : IQueryHandler<GetExpensesVouchersQuery, Pagination<ExpenseVoucherResponse>>
{
    public async Task<Result<Pagination<ExpenseVoucherResponse>>> Handle(GetExpensesVouchersQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new ExpenseVoucherSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<ExpenseVoucherResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<ExpenseVoucherResponse>(
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
            return Result.Failure<Pagination<ExpenseVoucherResponse>>(ExpenseVoucherErrores.ErrorConsulta);
        }
    }
}
