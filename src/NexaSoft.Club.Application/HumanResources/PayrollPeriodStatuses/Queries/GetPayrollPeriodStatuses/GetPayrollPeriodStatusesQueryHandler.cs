using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.PayrollPeriodStatuses;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.PayrollPeriodStatuses.Queries.GetPayrollPeriodStatuses;

public class GetPayrollPeriodStatusesQueryHandler(
    IGenericRepository<PayrollPeriodStatus> _repository
) : IQueryHandler<GetPayrollPeriodStatusesQuery, Pagination<PayrollPeriodStatusResponse>>
{
    public async Task<Result<Pagination<PayrollPeriodStatusResponse>>> Handle(GetPayrollPeriodStatusesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new PayrollPeriodStatusSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<PayrollPeriodStatusResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<PayrollPeriodStatusResponse>(
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
            return Result.Failure<Pagination<PayrollPeriodStatusResponse>>(PayrollPeriodStatusErrores.ErrorConsulta);
        }
    }
}
