using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.PayrollConfigs;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.PayrollConfigs.Queries.GetPayrollConfigs;

public class GetPayrollConfigsQueryHandler(
    IGenericRepository<PayrollConfig> _repository
) : IQueryHandler<GetPayrollConfigsQuery, Pagination<PayrollConfigResponse>>
{
    public async Task<Result<Pagination<PayrollConfigResponse>>> Handle(GetPayrollConfigsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new PayrollConfigSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<PayrollConfigResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<PayrollConfigResponse>(
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
            return Result.Failure<Pagination<PayrollConfigResponse>>(PayrollConfigErrores.ErrorConsulta);
        }
    }
}
