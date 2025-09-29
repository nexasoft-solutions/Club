using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Domain.Masters.Periodicities;
using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Application.Masters.Periodicities.Queries.GetPeriodicity;

public class GetPeriodicityQueryHandler(
    IGenericRepository<Periodicity> _repository
) : IQueryHandler<GetPeriodicityQuery, PeriodicityResponse>
{
    public async Task<Result<PeriodicityResponse>> Handle(GetPeriodicityQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new PeriodicitySpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
                if (entity is null)
                    return Result.Failure<PeriodicityResponse>(PeriodicityErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<PeriodicityResponse>(PeriodicityErrores.ErrorConsulta);
        }
    }
}
