using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Features.Proyectos.Cumplimientos;
using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Application.Features.Proyectos.Cumplimientos.Queries.GetCumplimiento;

public class GetCumplimientoQueryHandler(
    IGenericRepository<Cumplimiento> _repository
) : IQueryHandler<GetCumplimientoQuery, CumplimientoResponse>
{
    public async Task<Result<CumplimientoResponse>> Handle(GetCumplimientoQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new CumplimientoSpecification(specParams);
            var entity = await _repository.GetEntityWithSpec(spec, cancellationToken);
            if (entity is null)
                return Result.Failure<CumplimientoResponse>(CumplimientoErrores.NoEncontrado);

            return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<CumplimientoResponse>(CumplimientoErrores.ErrorConsulta);
        }
    }
}
