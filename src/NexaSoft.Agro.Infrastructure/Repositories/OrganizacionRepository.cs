using Microsoft.EntityFrameworkCore;
using NexaSoft.Agro.Application.Features.Organizaciones;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Masters.Constantes;
using NexaSoft.Agro.Domain.Features.Organizaciones;

namespace NexaSoft.Agro.Infrastructure.Repositories;

public class OrganizacionRepository(ApplicationDbContext _dbContext) : IOrganizacionRepository
{
   public async Task<(Pagination<OrganizacionResponse> Items, int TotalItems)> GetOrganizacionesAsync(ISpecification<Organizacion> spec, CancellationToken cancellationToken)
   {
      var queryBase = SpecificationEvaluator<Organizacion>.GetQuery(
         _dbContext.Set<Organizacion>().AsQueryable(), spec);

      var specParams = (spec as OrganizacionSpecification)?.SpecParams ?? new BaseSpecParams();

      var totalItems = await _dbContext.Set<Organizacion>()
         .Where((spec as OrganizacionSpecification)?.Criteria ?? (_ => true))
         .CountAsync(cancellationToken);

      var query = from c in queryBase

                  join Sector in _dbContext.Set<Constante>()
                      on new { Tipo = "Sector", Clave = c.SectorId }
                      equals new { Tipo = Sector.TipoConstante, Clave = Sector.Clave }
                      into SectorJoin
                  from SectorConst in SectorJoin.DefaultIfEmpty()

                  select new OrganizacionResponse(
                      c.Id,
                      c.NombreOrganizacion,
                      c.ContactoOrganizacion,
                      c.TelefonoContacto,
                      c.RucOrganizacion,
                      c.Observaciones,
                      SectorConst.Valor,
                      c.SectorId,
                      c.FechaCreacion,
                      c.FechaModificacion,
                      c.UsuarioCreacion,
                      c.UsuarioModificacion
                  );

      var items = await query.ToListAsync(cancellationToken);

      var pagination = new Pagination<OrganizacionResponse>(
            specParams.PageIndex,
            specParams.PageSize,
            totalItems,
            items
      );

      return (pagination, totalItems);

   }
}
