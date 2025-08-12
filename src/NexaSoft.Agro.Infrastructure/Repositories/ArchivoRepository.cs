using Microsoft.EntityFrameworkCore;
using NexaSoft.Agro.Application.Features.Proyectos.Archivos;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Masters.Constantes;
using NexaSoft.Agro.Domain.Features.Proyectos.Archivos;

namespace NexaSoft.Agro.Infrastructure.Repositories;

public class ArchivoRepository(ApplicationDbContext _dbContext) : IArchivoRepository
{
   public async Task<(Pagination<ArchivoResponse> Items, int TotalItems)> GetArchivosAsync(ISpecification<Archivo> spec, CancellationToken cancellationToken)
    {
         var queryBase = SpecificationEvaluator<Archivo>.GetQuery(
            _dbContext.Set<Archivo>().AsQueryable(), spec);

         var specParams = (spec as ArchivoSpecification)?.SpecParams ?? new BaseSpecParams();

         var totalItems = await _dbContext.Set<Archivo>()
            .Where((spec as ArchivoSpecification)?.Criteria ?? (_ => true))
            .CountAsync(cancellationToken);

         var query = from c in queryBase

            join TipoArchivo in _dbContext.Set<Constante>()
                on new { Tipo =  "TipoArchivo", Clave = c.TipoArchivoId }
                equals new { Tipo =  TipoArchivo.TipoConstante, Clave = TipoArchivo.Clave }
                into TipoArchivoJoin
            from TipoArchivoConst in TipoArchivoJoin.DefaultIfEmpty()

       

         select new ArchivoResponse(
             c.Id,
             c.NombreArchivo,
             c.DescripcionArchivo,
             c.RutaArchivo,
             c.FechaCarga,
             TipoArchivoConst.Valor,
             c.SubCapituloId,
             c.EstructuraId,
             c.FechaCreacion,
             c.TipoArchivoId
         );

      var items = await query.ToListAsync(cancellationToken);

      var pagination = new Pagination<ArchivoResponse>(
            specParams.PageIndex,
            specParams.PageSize,
            totalItems,
            items
      );

     return (pagination, totalItems);

    }
}
