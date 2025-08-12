using Microsoft.EntityFrameworkCore;
using NexaSoft.Agro.Application.Features.Proyectos.Estructuras;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Masters.Constantes;
using NexaSoft.Agro.Domain.Features.Proyectos.Estructuras;

namespace NexaSoft.Agro.Infrastructure.Repositories;

public class EstructuraRepository(ApplicationDbContext _dbContext) : IEstructuraRepository
{
   public async Task<(Pagination<EstructuraResponse> Items, int TotalItems)> GetEstructurasAsync(ISpecification<Estructura> spec, CancellationToken cancellationToken)
    {
         var queryBase = SpecificationEvaluator<Estructura>.GetQuery(
            _dbContext.Set<Estructura>().AsQueryable(), spec);

         var specParams = (spec as EstructuraSpecification)?.SpecParams ?? new BaseSpecParams();

         var totalItems = await _dbContext.Set<Estructura>()
            .Where((spec as EstructuraSpecification)?.Criteria ?? (_ => true))
            .CountAsync(cancellationToken);

         var query = from c in queryBase

            join TipoEstructura in _dbContext.Set<Constante>()
                on new { Tipo =  "TipoEstructura", Clave = c.TipoEstructuraId }
                equals new { Tipo =  TipoEstructura.TipoConstante, Clave = TipoEstructura.Clave }
                into TipoEstructuraJoin
            from TipoEstructuraConst in TipoEstructuraJoin.DefaultIfEmpty()

         select new EstructuraResponse(
             c.Id,
             TipoEstructuraConst.Valor,
             c.NombreEstructura,
             c.DescripcionEstructura,
             c.SubCapitulo!.NombreSubCapitulo!,
             c.PadreEstructuraId,
             c.SubCapituloId,
             c.TipoEstructuraId,
             c.FechaCreacion
         );

      var items = await query.ToListAsync(cancellationToken);

      var pagination = new Pagination<EstructuraResponse>(
            specParams.PageIndex,
            specParams.PageSize,
            totalItems,
            items
      );

     return (pagination, totalItems);

    }
}
