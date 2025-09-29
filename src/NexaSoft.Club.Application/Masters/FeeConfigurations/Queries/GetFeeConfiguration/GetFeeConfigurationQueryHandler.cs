using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Masters.FeeConfigurations;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Masters.FeeConfigurations.Queries.GetFeeConfiguration;

public class GetFeeConfigurationQueryHandler(
    IGenericRepository<FeeConfiguration> _repository
) : IQueryHandler<GetFeeConfigurationQuery, FeeConfigurationResponse>
{
    public async Task<Result<FeeConfigurationResponse>> Handle(GetFeeConfigurationQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new FeeConfigurationSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<FeeConfigurationResponse>(FeeConfigurationErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<FeeConfigurationResponse>(FeeConfigurationErrores.ErrorConsulta);
        }
    }
}
