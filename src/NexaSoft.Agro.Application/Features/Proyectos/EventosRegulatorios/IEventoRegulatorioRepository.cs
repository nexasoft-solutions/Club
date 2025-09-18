using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Features.Proyectos.EventosRegulatorios;

namespace NexaSoft.Agro.Application.Features.Proyectos.EventosRegulatorios;

public interface IEventoRegulatorioRepository
{
   Task<(Pagination<EventoRegulatorioResponse> Items, int TotalItems)> GetEventosRegulatoriosAsync(ISpecification<EventoRegulatorio> spec, CancellationToken cancellationToken); 
   
   Task<EventoRegulatorio?> GetByIdWithResponsablesAsync(long id, CancellationToken cancellationToken);
}
