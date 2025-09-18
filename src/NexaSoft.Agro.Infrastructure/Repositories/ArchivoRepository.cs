using Microsoft.EntityFrameworkCore;
using NexaSoft.Agro.Application.Features.Proyectos.Archivos;
using NexaSoft.Agro.Application.Abstractions.RequestHelpers;
using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Specifications;
using NexaSoft.Agro.Domain.Masters.Constantes;
using NexaSoft.Agro.Domain.Features.Proyectos.Archivos;
using NexaSoft.Agro.Domain.Features.Proyectos.SubCapitulos;
using NexaSoft.Agro.Domain.Features.Proyectos.Capitulos;
using NexaSoft.Agro.Domain.Features.Proyectos.Estructuras;

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
                      on new { Tipo = "TipoArchivo", Clave = c.TipoArchivoId }
                      equals new { Tipo = TipoArchivo.TipoConstante, Clave = TipoArchivo.Clave }
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
                      c.NombreCorto,
                      c.FechaCreacion,
                      c.TipoArchivoId,
                      c.FechaModificacion,
                      c.UsuarioCreacion,
                      c.UsuarioModificacion
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

   public async Task<List<ArchivoItemResponse>> GetArchivosByNombreCortoAsync(long estudioAmbientalId, string filtro, CancellationToken cancellationToken)
   {
      var query =
         from archivo in _dbContext.Set<Archivo>()

            // LEFT JOIN con SubCapítulo (directo)
         join subCapDirecto in _dbContext.Set<SubCapitulo>()
            on archivo.SubCapituloId equals subCapDirecto.Id into subCapDirectoJoin
         from subCapDirecto in subCapDirectoJoin.DefaultIfEmpty()

            // LEFT JOIN con Estructura
         join estructura in _dbContext.Set<Estructura>()
            on archivo.EstructuraId equals estructura.Id into estructuraJoin
         from estructura in estructuraJoin.DefaultIfEmpty()

            // LEFT JOIN con SubCapítulo desde Estructura
         join subCapDesdeEstructura in _dbContext.Set<SubCapitulo>()
            on estructura.SubCapituloId equals subCapDesdeEstructura.Id into subCapEstructuraJoin
         from subCapDesdeEstructura in subCapEstructuraJoin.DefaultIfEmpty()

            // LEFT JOIN con Capítulo desde SubCapítulo directo o desde estructura
         join capitulo in _dbContext.Set<Capitulo>()
            on (subCapDirecto != null ? subCapDirecto.CapituloId :
                  subCapDesdeEstructura != null ? subCapDesdeEstructura.CapituloId : 0)
            equals capitulo.Id

         // Filtros
         where capitulo.EstudioAmbientalId == estudioAmbientalId
               && EF.Functions.Like(archivo.NombreCorto!.ToLower(), $"%{filtro.ToLower()}%")

         select new ArchivoItemResponse
         (
            archivo.Id,
            // CapituloId
            subCapDirecto != null ? subCapDirecto.CapituloId :
                 subCapDesdeEstructura != null ? subCapDesdeEstructura.CapituloId : 0,

            // SubCapitulo efectivo (el que necesitas poblar sí o sí)
            subCapDirecto != null ? subCapDirecto.Id :
                 subCapDesdeEstructura != null ? subCapDesdeEstructura.Id : 0,

            // SubCapituloId original del archivo (puede ser null si vino desde estructura)
            archivo.SubCapituloId,

            archivo.EstructuraId,
            archivo.NombreCorto,
            estructura != null ? estructura.NombreEstructura : null
         );

      return await query.ToListAsync(cancellationToken);
   }

}
