using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.HumanResources.Positions;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.HumanResources.Positions.Queries.GetPositions;

public class GetPositionsQueryHandler(
    IGenericRepository<Position> _repository
) : IQueryHandler<GetPositionsQuery, Pagination<PositionResponse>>
{
    public async Task<Result<Pagination<PositionResponse>>> Handle(GetPositionsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new PositionSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<PositionResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<PositionResponse>(
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
            return Result.Failure<Pagination<PositionResponse>>(PositionErrores.ErrorConsulta);
        }
    }
}
