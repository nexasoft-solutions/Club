using Microsoft.EntityFrameworkCore;
using NexaSoft.Agro.Application.Features.Proyectos.EventosRegulatorios;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Masters.Constantes;
using NexaSoft.Agro.Domain.Features.Proyectos.EventosRegulatorios;

namespace NexaSoft.Agro.Infrastructure.Repositories;

public class EventoRegulatorioRepository(ApplicationDbContext _dbContext) : IEventoRegulatorioRepository
{
    public async Task<EventoRegulatorio?> GetByIdWithResponsablesAsync(long id, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<EventoRegulatorio>()
        .Include(e => e.Responsables)
        .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<(Pagination<EventoRegulatorioResponse> Items, int TotalItems)> GetEventosRegulatoriosAsync(ISpecification<EventoRegulatorio> spec, CancellationToken cancellationToken)
    {
        var queryBase = SpecificationEvaluator<EventoRegulatorio>.GetQuery(
           _dbContext.Set<EventoRegulatorio>().AsQueryable(), spec);

        var specParams = (spec as EventoRegulatorioSpecification)?.SpecParams ?? new BaseSpecParams();

        var totalItems = await _dbContext.Set<EventoRegulatorio>()
           .Where((spec as EventoRegulatorioSpecification)?.Criteria ?? (_ => true))
           .CountAsync(cancellationToken);

        var query = from c in queryBase

                    join TipoEvento in _dbContext.Set<Constante>()
                        on new { Tipo = "TipoEvento", Clave = c.TipoEventoId }
                        equals new { Tipo = TipoEvento.TipoConstante, Clave = TipoEvento.Clave }
                        into TipoEventoJoin
                    from TipoEventoConst in TipoEventoJoin.DefaultIfEmpty()

                    join FrecuenciaEvento in _dbContext.Set<Constante>()
                        on new { Tipo = "FrecuenciaEvento", Clave = c.FrecuenciaEventoId }
                        equals new { Tipo = FrecuenciaEvento.TipoConstante, Clave = FrecuenciaEvento.Clave }
                        into FrecuenciaEventoJoin
                    from FrecuenciaEventoConst in FrecuenciaEventoJoin.DefaultIfEmpty()

                    join EstadoEvento in _dbContext.Set<Constante>()
                        on new { Tipo = "EstadoEvento", Clave = c.EstadoEventoId }
                        equals new { Tipo = EstadoEvento.TipoConstante, Clave = EstadoEvento.Clave }
                        into EstadoEventoJoin
                    from EstadoEventoConst in EstadoEventoJoin.DefaultIfEmpty()

                    select new EventoRegulatorioResponse(
                        c.Id,
                        c.NombreEvento,
                        TipoEventoConst.Valor,
                        FrecuenciaEventoConst.Valor,
                        c.FechaExpedición,
                        c.FechaVencimiento,
                        c.Descripcion,
                        c.NotificarDíasAntes,
                        c.ResponsableId,
                        c.Responsable!.NombreResponsable,
                        c.Responsable!.CorreoResponsable,
                        c.Responsable!.TelefonoResponsable,
                        EstadoEventoConst.Valor,
                        c.EstudioAmbientalId,   
                        c.TipoEventoId,                     
                        c.FrecuenciaEventoId,
                        c.EstadoEventoId,
                        c.EstudioAmbiental!.Proyecto!,
                        c.FechaCreacion,
                        c.FechaModificacion,
                        c.UsuarioCreacion,
                        c.UsuarioModificacion,
                        c.Responsables
                    );

        var items = await query.ToListAsync(cancellationToken);

        var pagination = new Pagination<EventoRegulatorioResponse>(
              specParams.PageIndex,
              specParams.PageSize,
              totalItems,
              items
        );

        return (pagination, totalItems);

    }
}
