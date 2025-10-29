using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.PayrollPeriods;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.PayrollPeriods.Queries.GetPayrollPeriod;

public class GetPayrollPeriodQueryHandler(
    IGenericRepository<PayrollPeriod> _repository
) : IQueryHandler<GetPayrollPeriodQuery, PayrollPeriodResponse>
{
    public async Task<Result<PayrollPeriodResponse>> Handle(GetPayrollPeriodQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new PayrollPeriodSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<PayrollPeriodResponse>(PayrollPeriodErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<PayrollPeriodResponse>(PayrollPeriodErrores.ErrorConsulta);
        }
    }
}
