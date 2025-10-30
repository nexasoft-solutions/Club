using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.PayrollPeriodStatuses;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.PayrollPeriodStatuses.Queries.GetPayrollPeriodStatus;

public class GetPayrollPeriodStatusQueryHandler(
    IGenericRepository<PayrollPeriodStatus> _repository
) : IQueryHandler<GetPayrollPeriodStatusQuery, PayrollPeriodStatusResponse>
{
    public async Task<Result<PayrollPeriodStatusResponse>> Handle(GetPayrollPeriodStatusQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new PayrollPeriodStatusSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<PayrollPeriodStatusResponse>(PayrollPeriodStatusErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<PayrollPeriodStatusResponse>(PayrollPeriodStatusErrores.ErrorConsulta);
        }
    }
}
