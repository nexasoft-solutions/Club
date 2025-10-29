using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.PayrollConfigs;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.PayrollConfigs.Queries.GetPayrollConfig;

public class GetPayrollConfigQueryHandler(
    IGenericRepository<PayrollConfig> _repository
) : IQueryHandler<GetPayrollConfigQuery, PayrollConfigResponse>
{
    public async Task<Result<PayrollConfigResponse>> Handle(GetPayrollConfigQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new PayrollConfigSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<PayrollConfigResponse>(PayrollConfigErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<PayrollConfigResponse>(PayrollConfigErrores.ErrorConsulta);
        }
    }
}
