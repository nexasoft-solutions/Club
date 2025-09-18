using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Features.Proyectos.EventosRegulatorios;

namespace NexaSoft.Agro.Application.Features.Proyectos.EventosRegulatorios.Queries.GetEventoRegulatorio;

public class GetEventoRegulatorioQueryHandler(
    IEventoRegulatorioRepository _repository
) : IQueryHandler<GetEventoRegulatorioQuery, EventoRegulatorioResponse>
{
    public async Task<Result<EventoRegulatorioResponse>> Handle(GetEventoRegulatorioQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams { Id = query.Id };
            var spec = new EventoRegulatorioSpecification(specParams);

            var (pagination, _) = await _repository.GetEventosRegulatoriosAsync(spec, cancellationToken);

            var entity = pagination.Data.FirstOrDefault();

            if (entity is null)
               return Result.Failure<EventoRegulatorioResponse>(EventoRegulatorioErrores.NoEncontrado);

           return Result.Success(entity);
        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<EventoRegulatorioResponse>(EventoRegulatorioErrores.ErrorConsulta);
        }
    }
}
