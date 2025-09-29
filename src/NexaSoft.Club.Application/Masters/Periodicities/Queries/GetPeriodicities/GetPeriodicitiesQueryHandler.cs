using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Masters.Periodicities;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Masters.Periodicities.Queries.GetPeriodicities;

public class GetPeriodicitiesQueryHandler(
    IGenericRepository<Periodicity> _repository
) : IQueryHandler<GetPeriodicitiesQuery, Pagination<PeriodicityResponse>>
{
    public async Task<Result<Pagination<PeriodicityResponse>>> Handle(GetPeriodicitiesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new PeriodicitySpecification(query.SpecParams);
            var responses = await _repository.ListAsync<PeriodicityResponse>(spec,cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<PeriodicityResponse>(
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
            return Result.Failure<Pagination<PeriodicityResponse>>(PeriodicityErrores.ErrorConsulta);
        }
    }
}
