using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Masters.FeeConfigurations;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Masters.FeeConfigurations.Queries.GetFeeConfigurations;

public class GetFeeConfigurationsQueryHandler(
    IGenericRepository<FeeConfiguration> _repository
) : IQueryHandler<GetFeeConfigurationsQuery, Pagination<FeeConfigurationResponse>>
{
    public async Task<Result<Pagination<FeeConfigurationResponse>>> Handle(GetFeeConfigurationsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new FeeConfigurationSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<FeeConfigurationResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<FeeConfigurationResponse>(
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
            return Result.Failure<Pagination<FeeConfigurationResponse>>(FeeConfigurationErrores.ErrorConsulta);
        }
    }
}
