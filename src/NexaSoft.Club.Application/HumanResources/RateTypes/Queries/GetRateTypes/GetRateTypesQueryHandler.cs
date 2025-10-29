using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.RateTypes;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.RateTypes.Queries.GetRateTypes;

public class GetRateTypesQueryHandler(
    IGenericRepository<RateType> _repository
) : IQueryHandler<GetRateTypesQuery, Pagination<RateTypeResponse>>
{
    public async Task<Result<Pagination<RateTypeResponse>>> Handle(GetRateTypesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new RateTypeSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<RateTypeResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<RateTypeResponse>(
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
            return Result.Failure<Pagination<RateTypeResponse>>(RateTypeErrores.ErrorConsulta);
        }
    }
}
