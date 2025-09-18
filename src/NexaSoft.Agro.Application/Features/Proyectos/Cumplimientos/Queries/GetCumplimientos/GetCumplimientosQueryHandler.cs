using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Features.Proyectos.Cumplimientos;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Application.Features.Proyectos.Cumplimientos.Queries.GetCumplimientos;

public class GetCumplimientosQueryHandler(
    IGenericRepository<Cumplimiento> _repository
) : IQueryHandler<GetCumplimientosQuery, Pagination<CumplimientoResponse>>
{
    public async Task<Result<Pagination<CumplimientoResponse>>> Handle(GetCumplimientosQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new CumplimientoSpecification(query.SpecParams);
            var responses = await _repository.ListAsync<CumplimientoResponse>(spec, cancellationToken);
            var totalItems = await _repository.CountAsync(spec, cancellationToken);

            var pagination = new Pagination<CumplimientoResponse>(
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
            return Result.Failure<Pagination<CumplimientoResponse>>(CumplimientoErrores.ErrorConsulta);
        }
    }
}
