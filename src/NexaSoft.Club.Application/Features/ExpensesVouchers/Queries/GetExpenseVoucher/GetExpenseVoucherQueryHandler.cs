using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Features.ExpensesVouchers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Features.ExpensesVouchers.Queries.GetExpenseVoucher;

public class GetExpenseVoucherQueryHandler(
    IGenericRepository<ExpenseVoucher> _repository
) : IQueryHandler<GetExpenseVoucherQuery, ExpenseVoucherResponse>
{
    public async Task<Result<ExpenseVoucherResponse>> Handle(GetExpenseVoucherQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new ExpenseVoucherSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<ExpenseVoucherResponse>(ExpenseVoucherErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<ExpenseVoucherResponse>(ExpenseVoucherErrores.ErrorConsulta);
        }
    }
}
