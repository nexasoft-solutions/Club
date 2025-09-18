using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Features.Proyectos.EventosRegulatorios;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;

namespace NexaSoft.Agro.Application.Features.Proyectos.EventosRegulatorios.Queries.GetEventosRegulatorios;

public class GetEventosRegulatoriosQueryHandler(
    IEventoRegulatorioRepository _repository
) : IQueryHandler<GetEventosRegulatoriosQuery, Pagination<EventoRegulatorioResponse>>
{
    public async Task<Result<Pagination<EventoRegulatorioResponse>>> Handle(GetEventosRegulatoriosQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var spec = new EventoRegulatorioSpecification(query.SpecParams);
            var (pagination, _) = await _repository.GetEventosRegulatoriosAsync(spec, cancellationToken);

            return Result.Success(pagination);

        }
        catch (Exception ex)
        {
            var errores = ex.Message;
            return Result.Failure<Pagination<EventoRegulatorioResponse>>(EventoRegulatorioErrores.ErrorConsulta);
        }
    }
}
